using System.ComponentModel.DataAnnotations;
using WelderProjectManagement.Helpers;

namespace WelderProjectManagement.Dtos;

public class ProjectAddRequest
{
    [Required]
    public required ClientAddRequest Client { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Title { get; set; }

    [Required]
    [MaxLength(500)]
    public required string Description { get; set; }

    [Required]
    public required List<ProjectItemAddRequest> ProjectItems { get; set; }

    [Required]
    public required PaymentAddRequest Payment { get; set; }

    [Required]
    public required InvoiceAddRequest Invoice { get; set; }
}

public class ProjectResponse
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
