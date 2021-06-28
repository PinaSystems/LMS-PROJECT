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
    public class AssignmentRepository : DataRepository, IAssignmentRepository
    {
        private readonly IMapper _mapper;
        private readonly IImageWriter _writer;
        public AssignmentRepository(DataContext context, IMapper mapper, IImageWriter writer):base(context)
        {
            _writer= writer;
            _mapper= mapper;
        }

        public async Task<PagedList<AssignmentDto>> GetAssessments(AssignmentParams assignmentParams)
        {
            var assignments= await _context.AssignmentSubmissions
                                            .Include(item => item.Assignment)
                                            .ToListAsync();
            if(assignmentParams.Type!=0)
            {
                if(assignmentParams.Type== Enums.SubmissionStatusType.Pending)
                {
                    assignments=assignments.Where(p => p.SubmissionStatus==Enums.SubmissionStatusType.Pending)
                                            .ToList();
                }
                if(assignmentParams.Type== Enums.SubmissionStatusType.Submitted)
                {
                    assignments=assignments.Where(p => p.SubmissionStatus==Enums.SubmissionStatusType.Submitted)
                                            .ToList();
                }
            }
            var assignmentList= assignments.Select(p => p.Assignment).ToList();
            var assignmentForViewDto= new List<AssignmentDto>();
            _mapper.Map(assignmentList,assignmentForViewDto);
            return await PagedList<AssignmentDto>.CreateAsyncWithDtos(assignmentForViewDto.AsQueryable(), assignmentParams.Page, assignmentParams.Per_Page);
        }

        public async Task<PagedList<AssignmentDto>> GetStudentAssignments(AssignmentParams assignmentParams)
        {
            var assignments= await _context.AssignmentSubmissions
                                            .Include(item => item.Assignment)
                                            .Where(p => p.SubmissionStatus== Enums.SubmissionStatusType.Pending)
                                            .Select(p => p.Assignment)
                                            .ToListAsync();
            var assignmentForViewDto= new List<AssignmentDto>();
            _mapper.Map(assignments,assignmentForViewDto);
            return await PagedList<AssignmentDto>.CreateAsyncWithDtos(assignmentForViewDto.AsQueryable(), assignmentParams.Page, assignmentParams.Per_Page);
        }

        public async Task<AssignmentDto> GetAssignment(long assignmentId)
        {
            var assignment= await _context.Assignments.FirstOrDefaultAsync(item => item.Id.Equals(assignmentId));
            if(assignment==null)    return null;
            var assignmentViewDto= new AssignmentDto();
            _mapper.Map(assignment,assignmentViewDto);
            return assignmentViewDto;
        }

        public async Task<bool> UploadAssignment(IFormFile file, string assignmentJson)
        {
            string fileName=null;
            if(file!=null)
               fileName = await _writer.UploadFile(file);
            var assignment= JsonConvert.DeserializeObject<Assignment>(assignmentJson);
            assignment.AttachmentFile= fileName;
            _context.Assignments.Add(assignment);
            var res= await _context.SaveChangesAsync()>0?true:false;
            if(res)
            {
                // await EmailManager.SendEmailAsync("sajeesh.chatl@yahoo.com","","","");
                // EmailManager.SendMessage(new Batches()
                // {
                //     Text="Assignment has been received. Please check!",
                //     Number="9074328103"
                // });
                return true;
            }
            return false;
        }

        public async Task<bool> SubmitAssignment(IFormFile file, long userId, string assignmentJson)
        {
            string fileName=null;
            if(file!=null)
               fileName = await _writer.UploadFile(file);
            var assignment= JsonConvert.DeserializeObject<SubmitAssignmentDto>(assignmentJson);
            assignment.UserId= userId;
            var assignmentObj= await _context.AssignmentSubmissions
                                            .FirstOrDefaultAsync(item => item.AssignmentId.Equals(assignment.AssignmentId) &&
                                                                        item.StudentId== assignment.UserId);
            if(assignmentObj==null)    return false;
            _context.Entry(assignmentObj).Property(p => p.SubmissionStatus).CurrentValue= Enums.SubmissionStatusType.Submitted;
            _context.Entry(assignmentObj).Property(p => p.Comments).CurrentValue= assignment.Comments;
            var res= await _context.SaveChangesAsync()>0?true:false;
            if(res)
            {
                // await EmailManager.SendEmailAsync("sajeesh.chatl@yahoo.com","","","");
                // EmailManager.SendMessage(new Batches()
                // {
                //     Text="Assignment has been received. Please check!",
                //     Number="9074328103"
                // });
                return true;
            }
            return false;
        }

        public async Task<bool> ScoreAssignment(string assignmentJson)
        {
            // string fileName=null;
            // if(file!=null)
            //    fileName = await _writer.UploadFile(file);
            var assignment= JsonConvert.DeserializeObject<SubmitAssignmentDto>(assignmentJson);
            var assignmentObj= await _context.AssignmentSubmissions
                                            .FirstOrDefaultAsync(item => item.AssignmentId.Equals(assignment.AssignmentId) &&
                                                                        item.StudentId== assignment.UserId);
            if(assignmentObj==null)    return false;
            _context.Entry(assignmentObj).Property(p => p.SubmissionStatus).CurrentValue= Enums.SubmissionStatusType.Scored;
            _context.Entry(assignmentObj).Property(p => p.TrainerRemarks).CurrentValue= assignment.TrainerComments;
            _context.Entry(assignmentObj).Property(p => p.Score).CurrentValue= assignment.Score;
            var res= await _context.SaveChangesAsync()>0?true:false;
            if(res)
            {
                // await EmailManager.SendEmailAsync("sajeesh.chatl@yahoo.com","","","");
                // EmailManager.SendMessage(new Batches()
                // {
                //     Text="Assignment has been received. Please check!",
                //     Number="9074328103"
                // });
                return true;
            }
            return false;
        }
    }
}