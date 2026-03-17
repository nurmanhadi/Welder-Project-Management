using System.ComponentModel.DataAnnotations;

namespace WelderProjectManagement.Dtos;

public class ProjectItemAddRequest
{
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [Required]
    public required int Quantity { get; set; }

    [Required]
    public required decimal Price { get; set; }
}
