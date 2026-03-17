using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelderProjectManagement.Models;

public class Invoice
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public required string InvoiceNumber { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public required decimal Total { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public required decimal PaidAmount { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public required decimal Balance { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public required decimal ServiceFee { get; set; }

    public DateTime IssueDate { get; set; } = DateTime.UtcNow;

    [Required]
    public required DateTime DueDate { get; set; }

    [Required]
    public required long ProjectId { get; set; }

    [ForeignKey(nameof(ProjectId))]
    public Project Project { get; set; } = null!;
}
