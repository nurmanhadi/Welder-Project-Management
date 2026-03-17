using System.ComponentModel.DataAnnotations;

namespace WelderProjectManagement.Dtos;

public class InvoiceAddRequest
{
    [Required]
    public required DateTime DueDate { get; set; }

    [Required]
    public required decimal ServiceFee { get; set; }
}
