using Application.Contracts.Infrastructure;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.FileUpload;

public class FileUploader : IFileUpload
{
    private readonly Cloudinary _cloudinary;

    public FileUploader(IOptions<CloudinaryUrl> cloudinaryUrl)
    {
        // cloudinary configuration
        var cloudinary = cloudinaryUrl.Value;
        Console.WriteLine("url", cloudinary.cloudinaryUrl);
        _cloudinary = new Cloudinary(cloudinary.cloudinaryUrl);
    }

    public async Task<string> UploadImage(IFormFile file, string folderName)
    {
        var validationResult = ValidateFile(file);
        if (!string.IsNullOrEmpty(validationResult))
        {
            throw new Exception(validationResult);
        }
        
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = folderName,
        };

        try
        { 
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    // Validate the uploaded file
    private string ValidateFile(IFormFile? file)
    {
        if (file is not { Length: > 0 })
        {
            return "Please select a file.";
        }

        // Additional validation logic if needed (e.g., file size, file type
        // file size MB
        if (file.Length > 2097152)
        {
            throw new ValidationException("File size must not exceed 2 MB.");
        }
        
        
        var allowedExtensions = new[] { ".jpg", ".png", ".gif" }; // Add allowed extensions
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(fileExtension))
        {
            throw new ValidationException("Invalid file extension.");
        }

        // Additional validation logic can be added as needed

        return string.Empty; // No validation errors
    }
}