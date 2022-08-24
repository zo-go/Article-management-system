using Microsoft.Extensions.Configuration;
using WebAPI.Application.Common.Interface;
using WebAPI.Application.ReqDto;
using WebAPI.Application.ResDto;
using WebAPI.Domain.Entity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using WebAPI.Application.Configuration;

namespace WebAPI.Infrastructure.Identity
{

    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IRepository<AppIdentityUser> _appIdentityUserRepository;
        private AppUser? _appUser;

        public IdentityService(IConfiguration configuration, IRepository<AppUser> appUserRepository, IRepository<AppIdentityUser> appIdentityUserRepository)
        {
            _configuration = configuration;
            _appUserRepository = appUserRepository;
            _appIdentityUserRepository = appIdentityUserRepository;
        }
        public async Task<AppToken> GenerateToken()
        {
            var jwtSetting = _configuration.GetSection("JwtSetting").Get<JwtSetting>();
            var claims = new[]
           {
                new Claim("UserId",_appUser!.Id.ToString()),
                new Claim("Username",_appUser!.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(jwtSetting.Issuer, jwtSetting.Audience, claims, expires: DateTime.UtcNow.AddHours(jwtSetting.AccessExpiration), signingCredentials: credentials);

            //生成token
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            var refreshToken = GenerateRefreshToken();

            var tmpIdentityUser = new AppIdentityUser
            {
                UserId = _appUser.Id,
                Username = _appUser.Username,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(jwtSetting.RefreshExpiration)
            };
            // 根据用户的id，找到认证用户（登录用户）的所有记录，并硬删除，再更新
            var identityUsers = _appIdentityUserRepository.Table.Where(x => x.UserId == _appUser.Id).ToList();
            await _appIdentityUserRepository.DeleteBulkAsync(identityUsers, true);
            await _appIdentityUserRepository.UpdateAsync(tmpIdentityUser);

            var tokenp = new AppToken
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };

            var res = await Task<AppToken>.FromResult(tokenp);
            return res;
        }

        public async Task<AppToken> RefreshTokenAsync(AppToken appToken)
        {
            var jwtSetting = _configuration.GetSection("JwtSetting").Get<JwtSetting>();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSetting.Issuer,
                ValidAudience = jwtSetting.Audience,
                // ValidAudiences=au,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(appToken.AccessToken, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("token无效");
            }

            // 直接使用扩展方法拿到对应的值
            var userId = principal.FindFirstValue("UserId");

            if (userId != null)
            {
                var guserId = new Guid(userId);
                var user = _appIdentityUserRepository.Table.FirstOrDefault(x => x.UserId == guserId);
                if (user == null || user.RefreshToken != appToken.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now)
                {
                    throw new Exception("传入的token或者refreshToken无效");
                }

                //这里要记得重新获取下对应的用户相关信息
                _appUser = _appUserRepository.Table.FirstOrDefault(x => x.Id == guserId);
                return await GenerateToken();
            }
            else
            {
                throw new SecurityTokenException("token无效");
            }
        }

        public bool ValidateUserAsync(UserForAuth userForAuth)
        {
            _appUser = _appUserRepository.Table.FirstOrDefault(x => x.Username == userForAuth.Username
            && x.Password == userForAuth.Password);

            return _appUser is null ? false : true;
        }
        private string GenerateRefreshToken()
        {
            // 创建一个随机的Token用做Refresh Token
            var rndNumber = new byte[32];

            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(rndNumber);

            return Convert.ToBase64String(rndNumber);
        }
    }
}