using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OAWA.Data;
using OAWA.Data.Helpers;

namespace OAWA.API.Controllers
{
    [Route("api/[controller]")]
    public class CourseController: ControllerBase
    {
        private readonly ICourseRepository _courseRepo;
        public CourseController(ICourseRepository courseRepo)
        {
            _courseRepo= courseRepo;   
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery] CourseParams courseParams)
        {
            var courses= await _courseRepo.GetCourses(courseParams);
            if(courses!=null)
                return Ok(courses);
            return BadRequest();
        }

        [Authorize]
        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourse(long courseId)
        {
            var courses= await _courseRepo.GetCourse(courseId);
            if(courses!=null)
                return Ok(courses);
            return BadRequest();
        }
    }
}