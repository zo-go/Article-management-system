using System.Security.Claims;
using WebAPI.Application.Common.Interface;

namespace WebAPI.API.Service
{
    public class SessionUserService : ISessionUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid? UserId
        {
            get
            {
                var userIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue("UserId");
                if (userIdString != null)
                {
                    var userId = new Guid(userIdString);
                    return userId;
                }
                else
                {
                    return null;
                }

            }
        }

        public string? Username => _httpContextAccessor.HttpContext?.User.FindFirstValue("Username");
    }
}