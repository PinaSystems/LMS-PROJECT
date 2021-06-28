using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OAWA.Data;
using OAWA.Data.Helpers;

namespace OAWA.API.Controllers
{
    [Route("api/[controller]")]
    public class NuggetController: ControllerBase
    {
        private readonly INuggetRepository _repo;
        public NuggetController(INuggetRepository repo)
        {
            _repo= repo;   
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetNuggets([FromQuery] NuggetParams nuggetParams)
        {
            var nuggets= await _repo.GetNuggets(nuggetParams);
            if(nuggets!=null)
                return Ok(nuggets);
            return BadRequest();
        }
    }
}