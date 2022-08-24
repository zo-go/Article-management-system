using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Common.Interface;
using WebAPI.Application.ReqDto;
using WebAPI.Application.ResDto;
using WebAPI.Domain.Entity;
using WebAPI.Infrastructure.Persistence;
using WebAPI.Infrastructure.Persistence.EfRepository;
using WebAPI.Infrastructure.Utils;

namespace WebAPI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<AppUser> _user;
        private readonly IRepository<AppArticle> _article;
        private readonly IIdentityService _identityService;
        private readonly IRepository<AppUser> _appUserRespository;
        public UsersController(IRepository<AppUser> user, IIdentityService identityService, IRepository<AppUser> appUserRespository, IRepository<AppArticle> article)
        {
            _article = article;
            _user = user;
            _identityService = identityService;
            _appUserRespository = appUserRespository;
        }

        [AllowAnonymous]
        // 返回所有表
        [HttpGet]
        public dynamic GetUsersAsync([FromQuery] PageData pageData)
        {
            var pageSize = pageData.pageSize;
            var pageIndex = pageData.pageIndex;
            var keyword = pageData.keyword;
            var total = _user.Table.Count();
            var skinData = (pageSize) * (pageIndex - 1);

            if (pageData.pageSize == 0 && pageData.pageIndex == 0)
            {
                pageSize = 20;
                pageIndex = 1;
            }
            if (keyword == "" || keyword == null)
            {
                var users = _user.Table.OrderBy(x => x.CreatedAt);
                var users2 = users.Skip(skinData).Take(pageSize);
                return new
                {
                    code = 10000,
                    msg = "查询成功",
                    data = new
                    {
                        data = users2,
                        pageData = new
                        {
                            pageIndex,
                            pageSize,
                            total
                        }
                    }
                };
            }
            else
            {
                var users = _user.Table.OrderBy(x => x.CreatedAt).Where(x => x.Username.Contains(keyword)
                || x.Nickname != null && x.Nickname.Contains(keyword)
                || x.Email !=null && x.Email.Contains(keyword)
                || x.UserAddress != null && x.UserAddress.Contains(keyword));
                var users2 = users.Skip(skinData).Take(pageSize);
                return new
                {
                    code = 10000,
                    msg = "查询成功",
                    data = new
                    {
                        data = users2,
                        pageData = new
                        {
                            pageIndex,
                            pageSize,
                            total
                        }
                    }
                };
            }
            // var articles = _article.Table.Join(users,x=>x.AppUserId,p=>p.Id,(x,p)=>new {x.Id});

            // users.Join()
            // var users = _user.Table.Include(x => x.AppArticles);
        }

        // 返回单个用户
        [HttpGet("{username}")]
        public string user(string username)
        {
            var user = _user.Table.Where(x => x.Username == username).FirstOrDefault();
            var res = new
            {
                code = 10002,
                msg = "返回用户信息成功",
                data = user
            };
            return res.SerializeObject();
        }

        // 返回单个用户
        // [HttpGet("{id}")]
        // public async Task<AppUser> user(Guid id)
        // {
        //     var user = await _user.GetById(id);
        //     return user;
        // }


        [AllowAnonymous]
        // 添加
        [HttpPost]
        public String CreateUser(AppUser entity)
        {
            Console.WriteLine(entity);
            var user = _user.AddAsync(entity);
            var res = new
            {
                code = 10001,
                msg = "添加成功",
                data = user.Result
            };

            return res.SerializeObject();
        }

        // 修改
        [HttpPut]
        [Route("{id}")]
        public async Task<String> EditUser(Guid id, AppUser entity)
        {

            var userA = _user.GetById(id).Result;
            if (userA != null)
            {
                userA.Username = entity.Username;
                userA.Password = entity.Password;
                userA.Avatar = entity.Avatar;
                userA.Nickname = entity.Nickname;
                userA.Email = entity.Email;
                await _user.UpdateAsync(userA);
            }
            var res = new
            {
                code = 10002,
                msg = "修改成功",
                data = userA,
            };
            return res.SerializeObject();
        }

        // 删除
        [HttpDelete]
        [Route("{id}")]
        public async Task<string> DeleteUser(Guid id)
        {
            await _user.DeleteAsync(id);

            var res = new
            {
                code = 10002,
                msg = "删除成功",
                data = "",
            };
            return res.SerializeObject();
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<String> Authenticate([FromBody] UserForAuth userForAuth)
        {
            if (!_identityService.ValidateUserAsync(userForAuth))
            {
                return new { code = 4000, msg = "用户名或者密码不正确，请确认后重试！" }.SerializeObject();
            }

            var token = await _identityService.GenerateToken();

            var res = new
            {
                code = 1000,
                data = token,
                msg = "登录成功"
            };

            return res.SerializeObject();
        }

        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken(AppToken appToken)
        {
            var tokenToReturn = await _identityService.RefreshTokenAsync(appToken);
            return Ok(tokenToReturn);
        }


    }
}