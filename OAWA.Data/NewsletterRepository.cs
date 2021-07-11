using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Jupiter.Data.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OAWA.Data.Dtos;
using OAWA.Data.Helpers;
using OAWA.Data.Models;

namespace OAWA.Data
{
    public class NewsletterRepository : DataRepository, INewsletterRepository
    {
        private readonly IMapper _mapper;
        private readonly IImageWriter _writer;
        public NewsletterRepository(DataContext context, IMapper mapper, IImageWriter writer): base(context)
        {
            _mapper= mapper;
            _writer= writer;
        }
        public async Task<PagedList<NewsletterForViewDto>> GetNewsletters(NewsletterParams newsletterParams)
        {
            var newsletters= await _context.NewsLetters.ToListAsync();
            var newslettereForViewDto= new List<NewsletterForViewDto>();
            _mapper.Map(newsletters,newslettereForViewDto);
            return await PagedList<NewsletterForViewDto>.CreateAsyncWithDtos(newslettereForViewDto.AsQueryable(), newsletterParams.Page, newsletterParams.Per_Page);
        }

        public async Task<bool> UploadNewsletter(IFormFile file, string newsletterJson)
        {
            var fileName= await _writer.UploadFile(file);
            var newsletter= JsonConvert.DeserializeObject<NewsLetter>(newsletterJson);
            newsletter.FileName= fileName;
            _context.NewsLetters.Add(newsletter);
            var res= await _context.SaveChangesAsync()>0?true:false;
            if(res)
            {
                // await EmailManager.SendEmailAsync("sajeesh.chatl@yahoo.com","","","");
                // EmailManager.SendMessage(new Batches()
                // {
                //     Text="Newsletter has received. Please check!",
                //     Number="9074328103"
                // });
                return true;
            }
            return false;

        }
    }
}