using Domain.Entities.Enums;

namespace Domain.Entities.Models
{
    public class StudentProfile
    {
        public int Id { get; set; }
        public string PreviousDegree { get; set; } = string.Empty;
        public AcademicPerformanceLevel PerformanceLevel { get; set; }
        public List<string> AcademicInterests { get; set; } = [];
        public List<string> DesiredPrograms { get; set; } = [];
    }
}