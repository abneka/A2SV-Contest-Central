using Microsoft.AspNetCore.Http;

namespace Application.Contracts.Infrastructure;

public interface IFileUpload
{
    Task<string> UploadImage(IFormFile file, string folderName);
}