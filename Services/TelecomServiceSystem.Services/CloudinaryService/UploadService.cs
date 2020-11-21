namespace TelecomServiceSystem.Services.CloudinaryService
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class UploadService : IUploadService
    {
        private readonly Cloudinary cloudinary;

        public UploadService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadBillAsync(byte[] file)
        {
            using var destinationStream = new MemoryStream(file);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), destinationStream),
                Transformation = new Transformation().Width(1200).Height(1200).Background("white"),
            };

            var result = await this.cloudinary.UploadAsync(uploadParams);

            return result.Uri.AbsoluteUri;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            byte[] destinationImage;

            using var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);
            destinationImage = memoryStream.ToArray();

            using var destinationStream = new MemoryStream(destinationImage);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, destinationStream),
            };

            var result = await this.cloudinary.UploadAsync(uploadParams);

            return result.Uri.AbsoluteUri;
        }
    }
}
