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

public class ProjectItemsResponse
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
