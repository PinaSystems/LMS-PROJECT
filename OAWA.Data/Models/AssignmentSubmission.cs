using System;
using OAWA.Data.Enums;

namespace OAWA.Data.Models
{
    public class AssignmentSubmission
    {
        public long Id { get; set; }
        public long AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public long StudentId { get; set; }
        public User Student { get; set; }
        public string Comments { get; set; }
        public string File { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TrainerRemarks { get; set; }
        public int Score { get; set; }
        public SubmissionStatusType SubmissionStatus { get; set; }
    }
}