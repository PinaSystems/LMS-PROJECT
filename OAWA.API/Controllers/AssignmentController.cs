using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAWA.Data;
using OAWA.Data.Helpers;

namespace OAWA.API.Controllers
{
    [Route("api/[controller]")]
    public class AssignmentController: ControllerBase
    {
        private readonly IAssignmentRepository _assignmentRepo;
        public AssignmentController(IAssignmentRepository assignmentRepo)
        {
            _assignmentRepo= assignmentRepo;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetAssignmentsAll([FromQuery] AssignmentParams assignmentParams)
        {
            var assignments= await _assignmentRepo.GetAssessmentsAll(assignmentParams);
            if(assignments!=null)
                return Ok(assignments);
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAssignments([FromQuery] AssignmentParams assignmentParams)
        {
            var assignments= await _assignmentRepo.GetAssessments(assignmentParams);
            if(assignments!=null)
                return Ok(assignments);
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("students")]
        public async Task<IActionResult> GetStudentAssignments([FromQuery] AssignmentParams assignmentParams)
        {
            var userId= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            assignmentParams.UserId= userId;
            var assignments= await _assignmentRepo.GetStudentAssignments(assignmentParams);
            if(assignments!=null)
                return Ok(assignments);
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("{assignmentId}")]
        public async Task<IActionResult> GetAssignment(long assignmentId)
        {
            var assignment= await _assignmentRepo.GetAssignment(assignmentId);
            if(assignment!=null)
                return Ok(assignment);
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UploadAssignment(IFormFile file, [FromForm] string assignmentJson)
        {
            var assignment= await _assignmentRepo.UploadAssignment(file, assignmentJson);
            if(assignment)
            {
                return Ok(assignment);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitAssignment(IFormFile file, [FromForm] string assignmentJson)
        {
            var userId= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var assignment= await _assignmentRepo.SubmitAssignment(file, userId, assignmentJson);
            if(assignment)
            {
                return Ok(assignment);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("score")]
        public async Task<IActionResult> ScoreAssignment([FromForm] string assignmentJson)
        {
            var assignment= await _assignmentRepo.ScoreAssignment(assignmentJson);
            if(assignment)
            {
                return Ok(assignment);
            }
            return BadRequest();
        }
    }
}