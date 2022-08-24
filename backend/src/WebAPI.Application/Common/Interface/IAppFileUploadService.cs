using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Application.Common.Interface
{
    public interface IAppFileUploadService
    {
        Task<IEnumerable<Guid>> UploadFilesAsync(IFormCollection files);
        Task<FileContentResult> GetFileByIdAsync(Guid id);
    }
}