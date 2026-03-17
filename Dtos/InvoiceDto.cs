using System.ComponentModel.DataAnnotations;

namespace WelderProjectManagement.Dtos;

public class InvoiceAddRequest
{
    [Required]
    public required DateTime DueDate { get; set; }

    [Required]
    public required decimal ServiceFee { get; set; }
}

public class InvoiceResponse
{
    public long Id { get; set; }
    public string? InvoiceNumber { get; set; }
    public decimal Total { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal Balance { get; set; }
    public decimal ServiceFee { get; set; }
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
}
