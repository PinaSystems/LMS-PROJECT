namespace OAWA.Data.Dtos
{
    public class SubmitAssignmentDto
    {
        public string Comments { get; set; }
        public long UserId { get; set; }
        public long AssignmentId { get; set; }
        public string TrainerComments { get; set; }
        public int Score { get; set; }
    }
}