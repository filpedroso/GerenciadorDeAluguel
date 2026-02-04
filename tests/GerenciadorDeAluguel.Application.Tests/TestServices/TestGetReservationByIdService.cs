using Xunit;
using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Tests.InMemoryFakes;
using static GerenciadorDeAluguel.Domain.Tests.Builders.TestDataBuilder;

namespace GerenciadorDeAluguel.Tests.TestServices;

public class TestGetReservationByIdService
{
    [Fact]
    public async Task GetReservationById_ReturnsReservation_WhenExists()
    {
        var reservation = CreateValidReservation();
        var repo = new InMemoryReservationRepository();
        await repo.SaveAsync(reservation);
        
        var sut = new GetReservationByIdService(repo);
        
        var result = await sut.GetByIdAsync(reservation.Id);
        
        Assert.NotNull(result);
        Assert.Equal(reservation.Id, result.Id);
        Assert.Equal(reservation.Property.Id, result.Property.Id);
        Assert.Equal(reservation.Tenant.Id, result.Tenant.Id);
    }

    [Fact]
    public async Task GetReservationById_ThrowsException_WhenNotFound()
    {
        // Arrange
        var repo = new InMemoryReservationRepository();
        var sut = new GetReservationByIdService(repo);
        var nonExistentId = Guid.NewGuid();
        
        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await sut.GetByIdAsync(nonExistentId)
        );
    }
}
