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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ArticleClassController : Controller
    {
        private readonly IRepository<AppArticleClass> _appArticleClass;

        public ArticleClassController(IRepository<AppArticleClass> appArticleClass)
        {
            _appArticleClass = appArticleClass;
        }

        [AllowAnonymous]
        // 返回所有表
        [HttpGet]
        public string Index()
        {
            var articles = _appArticleClass.Table.ToArray().OrderBy(x => x.CreatedAt);

            var res = new
            {
                code = 30000,
                msg = "文章分类表",
                data = articles
            };
            return res.SerializeObject();
        }

        // 添加
        [HttpPost]
        public String CreateArticle(AppArticleClass entity)
        {
            // Console.WriteLine(entity);
            var appArticleClass = _appArticleClass.AddAsync(entity);
            var res = new
            {
                code = 30001,
                msg = "添加成功",
                data = appArticleClass.Result
            };

            return res.SerializeObject();
        }

        // 修改
        [HttpPut]
        [Route("{id}")]
        public async Task<String> EditUser(Guid id, AppArticleClass entity)
        {

            var appArticleClass = _appArticleClass.GetById(id).Result;
            if (appArticleClass != null)
            {
                appArticleClass.AppArticleClassName = entity.AppArticleClassName;
                await _appArticleClass.UpdateAsync(appArticleClass);
            }
            var res = new
            {
                code = 30002,
                msg = "修改成功",
                data = appArticleClass,
            };
            return res.SerializeObject();
        }

        // 删除
        [HttpDelete]
        [Route("{id}")]
        public async Task<string> DeleteUser(Guid id)
        {
            await _appArticleClass.DeleteAsync(id);

            var res = new
            {
                code = 30003,
                msg = "删除成功",
                data = "",
            };
            return res.SerializeObject();
        }
    }
}