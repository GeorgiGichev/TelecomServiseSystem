namespace TelecomServiceSystem.Services.CloudinaryService
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IUploadService
    {
        Task<string> UploadImageAsync(IFormFile file);

        Task<string> UploadBillAsync(byte[] file);
    }
}
