using WebAPI.Domain.Base;

namespace WebAPI.Domain.Entity
{
    public class AppUser : BaseEntity
    {
        // 用户名
        public string Username { get; set; } = null!;
        // 用户密码
        public string Password { get; set; } = null!;
        // 用户昵称
        public string? Nickname { get; set; } = null!;
        // 用户头像
        public string? Avatar { get; set; } = null!;
        // 用户性别
        public string? UserSex { get; set; }
        // 用户出生年月
        public DateTime? UserBirthday { get; set; }
        // 用户邮箱
        public string? Email { get; set; }
        // 用户住址
        public string? UserAddress { get; set; }
        // public List<AppArticle>? AppArticles{get;set;} = new List<AppArticle>();
        // public List<AppComment>? AppComments { get; set; } = new List<AppComment>();
    }
}