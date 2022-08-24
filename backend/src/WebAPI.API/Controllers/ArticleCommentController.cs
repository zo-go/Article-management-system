using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Application.Common.Interface;
using WebAPI.Domain.Entity;
using WebAPI.Infrastructure.Utils;

namespace WebAPI.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ArticleCommentController : Controller
    {
        private readonly IRepository<AppArticle> _appArticle;
        private readonly IRepository<AppUser> _user;
        private readonly IRepository<AppArticleClass> _articleClass;
        private readonly IRepository<AppComment> _appComment;

        public ArticleCommentController(IRepository<AppArticle> appArticle, IRepository<AppUser> user, IRepository<AppArticleClass> articleClass, IRepository<AppComment> appComment)
        {
            _appArticle = appArticle;
            _user = user;
            _articleClass = articleClass;
            _appComment = appComment;
        }

        // 返回评论表
        [AllowAnonymous]
        [HttpGet]
        public dynamic getArticleComment()
        {
            var appComment = _appComment.Table.ToList();
            return appComment;
        }


        // 根据文章id返回评论表
        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public dynamic getArticlesCommentById(Guid id)
        {

            var users = _user.Table;
            var appComment = _appComment.Table;
            var articles = _appArticle.Table.Join(appComment, x => x.Id, p => p.AppArticleId, (x, p) => new
            {
                x.Id,
                x.AppUserId,
                x.ArticleTitle,
                x.ArticleSecondTitle,
                x.ArticleContent,
                x.Document,
                x.viewstsNumber,
                x.CommentsNumber,
                x.AppArticleClassId,
                x.CreatedAt,
                x.ArticlePraise,
                p.CommentContent,
                p.UserCommentId
            }).Where(x=>x.Id == id);
            var articles2 = articles.Join(users, x => x.UserCommentId, p => p.Id, (x, p) => new
            {
                x.Id,
                x.AppUserId,
                x.ArticleTitle,
                x.ArticleSecondTitle,
                x.ArticleContent,
                x.Document,
                x.viewstsNumber,
                x.CommentsNumber,
                x.CreatedAt,
                x.CommentContent,
                p.Username,
                p.Avatar,
                p.Nickname,
                x.ArticlePraise,
                x.UserCommentId
            }).ToList();
            return articles2;
        }

        // 添加
        [HttpPost]
        public String CreateArticle(AppComment entity)
        {
            // Console.WriteLine(entity);
            var comment = _appComment.AddAsync(entity);
            var res = new
            {
                code = 40001,
                msg = "添加成功",
                data = comment.Result
            };

            return res.SerializeObject();
        }


        // 删除
        [HttpDelete]
        [Route("{id}")]
        public async Task<string> DeleteUser(Guid id)
        {
            await _appComment.DeleteAsync(id);

            var res = new
            {
                code = 40003,
                msg = "删除成功",
                data = "",
            };
            return res.SerializeObject();
        }
    }
}