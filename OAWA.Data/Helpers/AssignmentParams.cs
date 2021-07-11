using OAWA.Data.Enums;

namespace OAWA.Data.Helpers
{
    public class AssignmentParams: ListParams
    {
        public SubmissionStatusType Type { get; set; }
        public long UserId { get; set; }
    }
}