// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using WebAPI.Application.Common.Interface;
// using WebAPI.Infrastructure.Utils;

// namespace WebAPI.API.Controllers
// {
//     [Authorize]
//     [ApiController]
//     [Route("[controller]")]
//     public class IsGiveALike : Controller
//     {
//         private readonly IRepository<IsGiveALike> _isGiveALike;
//         public IsGiveALike(IRepository<IsGiveALike> isGiveALike)
//         {
//             _isGiveALike = isGiveALike;
//         }
//         // 返回所有表
//         [HttpGet]
//         public string Index()
//         {
//             var entity = _isGiveALike.Table.ToList();

//             var res = new
//             {
//                 code = 50000,
//                 msg = "点赞表",
//                 data = entity
//             };
//             return res.SerializeObject();
//         }

//         // 添加
//         [HttpPost]
//         public String CreateArticle(IsGiveALike entity)
//         {
//             // Console.WriteLine(entity);
//             var appArticleClass = _isGiveALike.AddAsync(entity);
//             var res = new
//             {
//                 code = 30001,
//                 msg = "添加成功",
//                 data = appArticleClass.Result
//             };

//             return res.SerializeObject();
//         }

//         // 修改
//         [HttpPut]
//         [Route("{id}")]
//         public async Task<String> EditUser(Guid id, IsGiveALike entity)
//         {

//             var entityOne = _isGiveALike.GetById(id).Result;
//             if (entityOne != null)
//             {
//                 entityOne.IsGiveALike = entity.IsGiveALike;
//                 await _isGiveALike.UpdateAsync(entity);
//             }
//             var res = new
//             {
//                 code = 30002,
//                 msg = "修改成功",
//                 data = entity,
//             };
//             return res.SerializeObject();
//         }
//     }
// }