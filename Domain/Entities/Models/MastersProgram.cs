using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities.Models
{
    // Root model that contains the array
    public class PostgraduateProgramsResponse
    {
        [JsonPropertyName("postgraduate_programs")]
        public List<PostgraduateProgram> PostgraduatePrograms { get; set; } = new List<PostgraduateProgram>();
    }

    // Individual program model
    public class PostgraduateProgram
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        [JsonPropertyName("program_name")]
        public string ProgramName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [JsonPropertyName("degree_type")]
        public string DegreeType { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [JsonPropertyName("study_mode")]
        public string StudyMode { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        [JsonPropertyName("field")]
        public string Field { get; set; } = string.Empty;

        // Additional properties you might want to add
        // public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        // public DateTime? ModifiedDate { get; set; }
        
    }
}