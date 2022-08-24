using WebAPI.Application.ReqDto;
using WebAPI.Application.ResDto;

namespace WebAPI.Application.Common.Interface
{
    public interface IIdentityService
    {
        bool ValidateUserAsync(UserForAuth userForAuth);

        Task<AppToken> GenerateToken();

        Task<AppToken> RefreshTokenAsync(AppToken appToken);
    }
}