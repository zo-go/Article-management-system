using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Application.Common.Interface;
using WebAPI.Application.ReqDto;
using WebAPI.Domain.Entity;
using WebAPI.Infrastructure.Utils;

namespace WebAPI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BlogsController : Controller
    {
        private readonly IRepository<AppArticle> _appArticle;
        private readonly IRepository<AppUser> _user;
        private readonly IRepository<AppArticleClass> _articleClass;

        public BlogsController(IRepository<AppArticle> appArticle, IRepository<AppUser> user, IRepository<AppArticleClass> articleClass)
        {
            _appArticle = appArticle;
            _user = user;
            _articleClass = articleClass;
        }

        [AllowAnonymous]
        // 返回所有表
        [HttpGet]
        public dynamic getArticles([FromQuery] PageData pageData)
        {
            // var articles = _appArticle.Table.ToArray();
            // var articles = _appArticle.Table.Include(x=>x.AppUsers).ToList();
            var pageSize = pageData.pageSize;
            var pageIndex = pageData.pageIndex;
            var keyword = pageData.keyword;
            var total = _appArticle.Table.Count();
            var skinData = (pageSize) * (pageIndex - 1);
            if (pageData.pageSize == 0 && pageData.pageIndex == 0)
            {
                pageSize = 20;
                pageIndex = 1;
            }
            if (keyword == "" || keyword == null)
            {
                var users = _user.Table;
                var articleClass = _articleClass.Table;
                var articles = _appArticle.Table.Join(users, x => x.AppUserId, p => p.Id, (x, p) => new
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
                    p.Username,
                    p.Avatar,
                    p.Nickname,
                    x.ArticlePraise
                }).Skip(skinData).Take(pageSize).OrderByDescending(x => x.CreatedAt);
                var articles2 = articles.Join(articleClass, x => x.AppArticleClassId, p => p.Id, (x, p) => new
                {
                    x.Id,
                    x.AppUserId,
                    x.ArticleTitle,
                    x.ArticleSecondTitle,
                    x.ArticleContent,
                    x.Document,
                    x.viewstsNumber,
                    x.CommentsNumber,
                    p.AppArticleClassName,
                    x.CreatedAt,
                    x.Username,
                    x.Avatar,
                    x.Nickname,
                    x.ArticlePraise
                });
                // var articles3 = articles2.Skip(skinData).Take(pageSize);

                return new
                {
                    code = 20000,
                    msg = "查询成功",
                    data = new
                    {
                        data = articles2,
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
                var users = _user.Table;
                var articleClass = _articleClass.Table;
                var articles = _appArticle.Table.Join(users, x => x.AppUserId, p => p.Id, (x, p) => new
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
                    p.Username,
                    p.Avatar,
                    p.Nickname,
                    x.ArticlePraise
                });
                var articles2 = articles.Join(articleClass, x => x.AppArticleClassId, p => p.Id, (x, p) => new
                {
                    x.Id,
                    x.AppUserId,
                    x.ArticleTitle,
                    x.ArticleSecondTitle,
                    x.ArticleContent,
                    x.Document,
                    x.viewstsNumber,
                    x.CommentsNumber,
                    p.AppArticleClassName,
                    x.CreatedAt,
                    x.Username,
                    x.Avatar,
                    x.Nickname,
                    x.ArticlePraise
                }).OrderBy(x => x.CreatedAt).Where(m => m.Username.Contains(keyword)
                || m.ArticleTitle.Contains(keyword)
                || m.Nickname != null && m.Nickname.Contains(keyword)
                || m.AppArticleClassName !=null && m.AppArticleClassName.Contains(keyword));

                return new
                {
                    code = 20000,
                    msg = "查询成功",
                    data = new
                    {
                        data = articles2,
                        pageData = new
                        {
                            pageIndex,
                            pageSize,
                            total
                        }
                    }
                };
            }
            // var pageSize = pageData.pageSize;
            // var pageIndex = pageData.pageIndex;
        }

        [AllowAnonymous]
        // 查找
        [HttpGet]
        [Route("{id}")]
        public dynamic getArticlesById(Guid id)
        {

            var users = _user.Table;
            var articleClass = _articleClass.Table;
            var articles = _appArticle.Table.Join(users, x => x.AppUserId, p => p.Id, (x, p) => new
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
                p.Username,
                p.Avatar,
                p.Nickname,
            }).Where(x => x.Id == id).ToList();
            var articles2 = articles.Join(articleClass, x => x.AppArticleClassId, p => p.Id, (x, p) => new
            {
                x.Id,
                x.AppUserId,
                x.ArticleTitle,
                x.ArticleSecondTitle,
                x.ArticleContent,
                x.Document,
                x.viewstsNumber,
                x.CommentsNumber,
                p.AppArticleClassName,
                x.CreatedAt,
                x.Username,
                x.Avatar,
                x.Nickname,
                x.ArticlePraise
            }).ToList();
            return articles2;
        }
        // 添加
        [HttpPost]
        public String CreateArticle(AppArticle entity)
        {
            // Console.WriteLine(entity);
            var articles = _appArticle.AddAsync(entity);
            var res = new
            {
                code = 20001,
                msg = "添加成功",
                data = articles.Result
            };

            return res.SerializeObject();
        }

        // 修改
        [HttpPut]
        [Route("{id}")]
        public async Task<String> EditUser(Guid id, AppArticle entity)
        {

            var articles = _appArticle.GetById(id).Result;
            if (articles != null)
            {
                articles.ArticleTitle = entity.ArticleTitle;
                articles.ArticleSecondTitle = entity.ArticleSecondTitle;
                articles.ArticleContent = entity.ArticleContent;
                articles.Document = entity.Document;
                articles.viewstsNumber = entity.viewstsNumber;
                articles.CommentsNumber = entity.CommentsNumber;
                articles.ArticlePraise = entity.ArticlePraise;
                await _appArticle.UpdateAsync(articles);
            }
            var res = new
            {
                code = 20002,
                msg = "修改成功",
                data = articles,
            };
            return res.SerializeObject();
        }

        // 删除
        [HttpDelete]
        [Route("{id}")]
        public async Task<string> DeleteUser(Guid id)
        {
            await _appArticle.DeleteAsync(id);

            var res = new
            {
                code = 20003,
                msg = "删除成功",
                data = "",
            };
            return res.SerializeObject();
        }

    }
}