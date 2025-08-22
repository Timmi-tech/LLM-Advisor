namespace Domain.Entities.Models
{
    public class ConversationState
    {
        public int Step { get; set; } = 0;
        public StudentProfile Student { get; set; } = new StudentProfile();
    }
}