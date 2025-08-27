using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class PaginationParametersDto
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 10;
    }
}