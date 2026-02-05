using GerenciadorDeAluguel.Domain.Enums;

namespace GerenciadorDeAluguel.Application.DTOs;

/// <summary>
/// Data transfer object for changing a property's status.
/// </summary>
public class ChangePropertyStatusDTO
{
    /// <summary>
    /// New status to assign (Available, Rented, UnderMaintenance, Unavailable).
    /// </summary>
    public PropertyStatus NewStatus { get; set; }
}
