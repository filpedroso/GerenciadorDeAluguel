using GerenciadorDeAluguel.Domain.Enums;

namespace GerenciadorDeAluguel.Application.DTOs;

public class RegisterPropertyDTO
{
    public PropertyType Type { get; set; }
    
    // Address components
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string? Complement { get; set; }
    
    // Price components
    public decimal MonthlyRent { get; set; }

    // Owner
    public Guid OwnerId { get; set; }
}
