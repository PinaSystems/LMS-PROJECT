using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OAWA.Data.Helpers
{
    public interface IImageWriter
    {
        Task<string> UploadFile(IFormFile file);
        Task<string> UploadImage(IFormFile file);
        Task<string> UploadDoc(IFormFile file);
        Task<string> UploadVideo(IFormFile file);

    }
}