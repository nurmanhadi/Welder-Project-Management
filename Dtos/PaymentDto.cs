using System.ComponentModel.DataAnnotations;
using WelderProjectManagement.Helpers;

namespace WelderProjectManagement.Dtos;

public class PaymentAddRequest
{
    [Required]
    public required decimal Amount { get; set; }

    [Required]
    public required PaymentType Type { get; set; }
}
public class PaymentResponse
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public PaymentType Type { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}
