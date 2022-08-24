namespace WebAPI.Application.Configuration
{
    public class UploadFileSetting
    {
        public string FilePath { get; set; } = null!;
        public string AllowFileExtensions { get; set; } = null!;
        public long AllowMaxFileSize { get; set; }
    }
}