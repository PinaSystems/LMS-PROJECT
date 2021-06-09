using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using OAWA.Data.Dtos;
using OAWA.Data.Helpers;

namespace OAWA.Data
{
    public interface INewsletterRepository
    {
        Task<PagedList<NewsletterForViewDto>> GetNewsletters(NewsletterParams newsletterParams);
        Task<bool> UploadNewsletter(IFormFile file, string newsletterJson);
    }
}