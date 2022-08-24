using WebAPI.Domain.Base;

namespace WebAPI.Domain.Entity
{
    public class AppFileInformation : BaseEntity
    {
        public string OriginFileName { get; set; } = null!;
        public string CurrentFileName { get; set; } = null!;
        public string RelativePath { get; set; } = null!;
        public long FileSize { get; set; }
        public string FileType { get; set; } = null!;
    }
}