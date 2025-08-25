using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UpdateProgramDto
    {
        [Required]
        [StringLength(500)]
        public string ProgramName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string DegreeType { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string StudyMode { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Field { get; set; } = string.Empty;
    }
}