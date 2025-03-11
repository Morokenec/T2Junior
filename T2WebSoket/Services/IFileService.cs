using T2WebSoket.DTOs;
using T2WebSoket.Models;

namespace T2WebSoket.Services
{
    public interface IFileService
    {
        Task<ChatFile> UploadFileAsync(FileUploadDTO fileUpload);
        Task<FileDTO> GetFileByIdAsync(Guid fileId);
        Task<bool> DeleteFileAsync(Guid fileId);
    }
}
