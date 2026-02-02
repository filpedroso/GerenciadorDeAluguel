using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Tests.InMemoryFakes;
using Xunit;
using static GerenciadorDeAluguel.Tests.Builders.TestDataBuilder;

namespace GerenciadorDeAluguel.Tests.TestServices
{
    public class TestCreateReservationService
    {
        [Fact]
        public async Task CreateReservation_SavesReservation_WhenRequestIsValid()
        {
            var property = CreateValidProperty(owner: CreateValidOwner());
            var client = CreateValidClient();

            var propertyRepo = new InMemoryPropertyRepository();
            propertyRepo.Add(property);
            
            var clientRepo = new InMemoryClientRepository();
            clientRepo.Add(client);
            
            var reservationRepo = new InMemoryReservationRepository();
            var sut = new CreateReservationService(clientRepo, propertyRepo, reservationRepo);
            
            var command = new CreateReservationCommandDTO(
                PropertyId: property.Id,
                ClientId: client.Id,
                CheckIn: new DateOnly(2026, 2, 1),
                CheckOut: new DateOnly(2026, 2, 28)
            );

            // Act
            var result = await sut.CreateAsync(command);

            Assert.NotEqual(Guid.Empty, result);
            Assert.Equal(1, reservationRepo.Count);

            var savedReservation = reservationRepo.GetById(result);

            Assert.NotNull(savedReservation);
            Assert.Equal(property.Id, savedReservation.Property.Id);
            Assert.Equal(client.Id, savedReservation.Tenant.Id);
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
