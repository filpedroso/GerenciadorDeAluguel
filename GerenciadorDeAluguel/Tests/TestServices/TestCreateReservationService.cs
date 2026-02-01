using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Application.Services;
using Xunit;

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

/*
Step 2: Create Application/DTOs/CreateReservationCommand.cs:

csharp
namespace GerenciadorDeAluguel.Application.DTOs;

public sealed record CreateReservationCommand(
    Guid PropertyId,
    Guid ClientId,
    DateOnly CheckIn,
    DateOnly CheckOut);

Step 3: Create Application/Ports/IPropertyRepository.cs (and the other two interfaces)

Step 4: Create Application/Services/CreateReservationService.cs (empty shell)

Step 5: Create in-memory fakes in Tests/Fakes/ so your test can run

Step 6: Uncomment the test code → RED (compiles but service does nothing)

Step 7: Implement service → GREEN

Want me to show you just Step 2-4 (the minimal files to make it compile), or do you want to try creating them based on what we discussed?
*/
