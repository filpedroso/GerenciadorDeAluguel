using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Tests.InMemoryFakes;
using GerenciadorDeAluguel.Domain.Tests.Builders;
using Xunit;

namespace GerenciadorDeAluguel.Tests.TestServices;

public class TestListReservationsService
{
    private readonly InMemoryReservationRepository _reservationRepository;
    private readonly ListReservationsService _service;

    public TestListReservationsService()
    {
        _reservationRepository = new InMemoryReservationRepository();
        _service = new ListReservationsService(_reservationRepository);
    }

    [Fact]
    public async Task ShouldReturnAllReservations()
    {
        // Arrange
        var r1 = TestDataBuilder.CreateValidReservation();
        var r2 = TestDataBuilder.CreateValidReservation();

        await _reservationRepository.SaveAsync(r1);
        await _reservationRepository.SaveAsync(r2);

        // Act
        var all = await _service.ListAsync();

        // Assert
        Assert.Contains(all, r => r.Id == r1.Id);
        Assert.Contains(all, r => r.Id == r2.Id);
    }
}
