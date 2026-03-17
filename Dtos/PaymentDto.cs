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
