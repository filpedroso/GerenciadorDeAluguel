using Xunit;
using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Application.Ports;

namespace GerenciadorDeAluguel.Tests.TestServices
{
    public class TestCreateReservationService
    {
        [Fact]
        public async Task CreateReservation_SavesReservation_WhenRequestIsValid()
        {
            // This test WILL FAIL because nothing exists yet!
            
            // Arrange
            var propertyId = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            var command = new CreateReservationCommand(
                PropertyId: propertyId,
                ClientId: clientId,
                CheckIn: new DateOnly(2026, 2, 1),
                CheckOut: new DateOnly(2026, 2, 28)
            );

            // TODO: Create fake repositories (next step)
            // var propertyRepo = new InMemoryPropertyRepository();
            // var clientRepo = new InMemoryClientRepository();
            // var reservationRepo = new InMemoryReservationRepository();
            
            // TODO: Add test data
            // propertyRepo.Add(new Property(...));
            // clientRepo.Add(new Client(...));

            // var sut = new CreateReservationService(propertyRepo, clientRepo, reservationRepo);

            // Act
            // var result = await sut.CreateAsync(command);

            // Assert
            // Assert.NotEqual(Guid.Empty, result.ReservationId);
            // Assert.Equal(1, reservationRepo.Count);
        }
    }
}
