using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OAWA.Data.Dtos;
using OAWA.Data.Helpers;

namespace OAWA.Data
{
    public interface IAssignmentRepository
    {
        Task<PagedList<AssignmentDto>> GetAssessmentsAll(AssignmentParams assignmentParams);
        Task<PagedList<AssignmentDto>> GetAssessments(AssignmentParams assignmentParams);
        Task<PagedList<AssignmentDto>> GetStudentAssignments(AssignmentParams assignmentParams);
        Task<bool> UploadAssignment(IFormFile file, string assignmentJson);
        Task<AssignmentDto> GetAssignment(long assignmentId);
        Task<bool> SubmitAssignment(IFormFile file, long userId, string assignmentJson);
        Task<bool> ScoreAssignment(string assignmentJson);
    }
}