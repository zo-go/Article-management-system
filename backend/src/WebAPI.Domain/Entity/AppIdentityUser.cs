namespace WebAPI.Domain.Entity
{
    /// <summary>
    /// 认证用户信息（登录用户信息）
    /// </summary>
    public class AppIdentityUser : Base.BaseEntity
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiration { get; set; }
    }
}