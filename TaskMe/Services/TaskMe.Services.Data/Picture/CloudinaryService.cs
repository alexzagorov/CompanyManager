namespace TaskMe.Services.Data.Picture
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadPhotoAsync(IFormFile picture, string name)
        {
            this.cloudinary.Api.UrlImgUp.Transform(new Transformation()
            .Width(150).Height(150).Gravity("face").Radius(20).Crop("thumb"));

            byte[] destinationData;

            using (var ms = new MemoryStream())
            {
                await picture.CopyToAsync(ms);
                destinationData = ms.ToArray();
            }

            UploadResult uploadResult = null;

            using (var ms = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "company_images",
                    File = new FileDescription(name, ms),
                };

                uploadResult = this.cloudinary.Upload(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
