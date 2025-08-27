using System.Text;
using Domain.Entities.Models;

namespace Application.Services
{
    public static class PromptBuilder
    {
        public static string BuildPrompt(StudentProfile student, List<PostgraduateProgram> programs)
        {
            var prompt = new StringBuilder();
            prompt.AppendLine("You are an expert academic advisor for the University of Lagos.");
            prompt.AppendLine("Analyze the student's profile carefully and recommend the **top 3 most suitable Master's programs**.");
            prompt.AppendLine("For each recommendation, provide:");
            prompt.AppendLine("- ProgramName");
            prompt.AppendLine("- A detailed Reason (cover relevance, academic fit, and career prospects).");
            prompt.AppendLine();
            prompt.AppendLine("Student Profile:");
            prompt.AppendLine($"- Previous Degree: {student.PreviousDegree}");
            prompt.AppendLine($"- Performance: {student.PerformanceLevel}");
            prompt.AppendLine($"- Academic Interests: {string.Join(", ", student.AcademicInterests)}");
            var desiredPrograms = student.DesiredPrograms?.Any() == true ? string.Join(", ", student.DesiredPrograms) : "Not specified";
            prompt.AppendLine($"- Preferred Course of Study: {desiredPrograms}");
            prompt.AppendLine();
            prompt.AppendLine("Available Programs at University of Lagos:");
            foreach (var program in programs)
            {
                prompt.AppendLine($"- {program.ProgramName} ({program.DegreeType}, {program.StudyMode}, {program.Field})");
            }
            prompt.AppendLine();
            prompt.AppendLine("Return the output strictly in JSON format like this:");
            prompt.AppendLine(@"[
  { ""ProgramName"": ""..."", ""Reason"": ""Provide a detailed explanation here..."" },
  { ""ProgramName"": ""..."", ""Reason"": ""Provide a detailed explanation here..."" },
  { ""ProgramName"": ""..."", ""Reason"": ""Provide a detailed explanation here..."" }
]");
            return prompt.ToString();
        }
    }
}
