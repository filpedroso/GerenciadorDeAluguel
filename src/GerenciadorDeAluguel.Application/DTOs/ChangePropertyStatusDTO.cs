using GerenciadorDeAluguel.Domain.Enums;

namespace GerenciadorDeAluguel.Application.DTOs;

public class ChangePropertyStatusDTO
{
    public PropertyStatus NewStatus { get; set; }
}
