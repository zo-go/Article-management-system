using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Base;

namespace WebAPI.Domain.Entity
{
    public class AppComment : BaseEntity
    {
        // 评论者id
        public Guid UserCommentId {get;set;}
        // 评论内容
        public String? CommentContent {get;set;}
        // 外键 用户id
        public Guid AppUserId {get;set;}
        public AppUser? AppUsers {get;set;}
        // 文章id
        public Guid AppArticleId {get;set;}
        // public AppArticle? AppArticle {get;set;}
    }
}