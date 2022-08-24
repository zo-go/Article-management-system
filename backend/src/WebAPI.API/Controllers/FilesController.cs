using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Common.Interface;
using WebAPI.Infrastructure.Utils;

namespace Admin2022Soft1.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class FilesController : Controller
    {
        private readonly IAppFileUploadService _appFileService;

        public FilesController(IAppFileUploadService appFileService)
        {
            _appFileService = appFileService;
        }


        [HttpPost]
        public async Task<string> UploadFile(IFormCollection uploadFiles)
        {
            var list = await _appFileService.UploadFilesAsync(uploadFiles);

            if (list.Count() > 0)
            {
                var res = new
                {
                    code = 1000,
                    msg = "上传成功",
                    data = list
                };

                return res.SerializeObject();
            }
            else
            {
                var res = new
                {
                    code = 4000,
                    msg = "上传失败，有不允许上传的文件扩展名或超出允许的文件大小",
                    data = list
                };

                return res.SerializeObject();
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<FileContentResult> GetFile(Guid id)
        {
            var f = await _appFileService.GetFileByIdAsync(id);
            return f;
        }
    }
}