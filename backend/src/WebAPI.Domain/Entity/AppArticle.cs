using WebAPI.Domain.Base;

namespace WebAPI.Domain.Entity
{
    public class AppArticle : BaseEntity
    {
        // 文章标题
        public string ArticleTitle { get; set; } = null!;
        // 文章副标题
        public string? ArticleSecondTitle { get; set; }
        // 文章内容
        public string? ArticleContent { get; set; }
        // 文章封面
        public string? Document { get; set; }
        // 外键 文章分类id
        public Guid AppArticleClassId { get; set; }
        public AppArticleClass? AppArticleClasses { get; set; }
        // 外键 用户id
        public Guid AppUserId { get; set; }
        public AppUser? AppUsers { get; set; }
        // 浏览次数
        public long viewstsNumber { get; set; }
        // 评论次数
        public long CommentsNumber { get; set; }
        // 点赞次数
        public long ArticlePraise { get; set; }

        // public List<AppComment>? AppComments { get; set; } = new List<AppComment>();
    }
}