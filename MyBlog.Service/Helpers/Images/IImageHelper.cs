using Microsoft.AspNetCore.Http;
using MyBlog.Entity.DTOs.Images;
using MyBlog.Entity.Enums;

namespace MyBlog.Service.Helpers.Images
{
    public interface IImageHelper
    {
        Task<ImageUploadedDto> Upload(string? name, IFormFile? imageFile, ImageType? imageType, string folderName = null);

        void Delete(string imageName);

    }
}
