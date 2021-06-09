using System.Threading.Tasks;
using Jupiter.Data.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using OAWA.Data;
using OAWA.Data.Helpers;

namespace OAWA.API.Controllers
{
    [Route("api/[controller]")]
    public class NewsLetterController: ControllerBase
    {
        private readonly INewsletterRepository _newsletterRepo;
        public NewsLetterController(INewsletterRepository newsletterRepo)
        {
            _newsletterRepo= newsletterRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetNewsletters([FromQuery] NewsletterParams newsletterParams)
        {
            var newsletters= await _newsletterRepo.GetNewsletters(newsletterParams);
            if(newsletters!=null)
                return Ok(newsletters);
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UploadNewsletters(IFormFile file, [FromForm] string newsletterJson)
        {
            var newsletters= await _newsletterRepo.UploadNewsletter(file, newsletterJson);
            if(newsletters)
            {
                return Ok(newsletters);
            }
            return BadRequest();
        }
    }
}