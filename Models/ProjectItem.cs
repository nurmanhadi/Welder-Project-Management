using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelderProjectManagement.Models;

public class ProjectItem
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public required string Name { get; set; }

    [Required]
    public required int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public required decimal Price { get; set; }

    [Required]
    public required long ProjectId { get; set; }

    [ForeignKey(nameof(ProjectId))]
    public Project Project { get; set; } = null!;
}
