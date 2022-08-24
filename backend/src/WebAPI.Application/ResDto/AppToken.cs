
namespace WebAPI.Application.ResDto
{
    public class AppToken
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}