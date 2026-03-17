using System.ComponentModel.DataAnnotations;

namespace WelderProjectManagement.Dtos;

public class ClientAddRequest
{
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(20)]
    public required string Phone { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Address { get; set; }
}

public class ClientUpdateRequest
{
    [MaxLength(50)]
    public string? Name { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }
}

public class ClientResponse
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
}
