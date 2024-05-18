namespace DATN_back_end.Common.FileService
{
    public interface IFileService
    {
        Task<string> UploadFileGetUrlAsync(IFormFile file);
        Task DeleteFileAsync(List<string> fileNames);

    }
}
