using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WelderProjectManagement.Helpers;

namespace WelderProjectManagement.Models;

public class Project
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public required string Title { get; set; }

    [Required]
    [MaxLength(500)]
    [Column(TypeName = "varchar(500)")]
    public required string Description { get; set; }

    [Required]
    [MaxLength(20)]
    [Column(TypeName = "varchar(20)")]
    public required ProjectStatus Status { get; set; }


    [Required]
    public required long ClientId { get; set; }

    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ProjectItem> ProjectItems { get; set; } = [];
    public ICollection<Payment> Payments { get; set; } = [];
    public Invoice? Invoice { get; set; }
}
