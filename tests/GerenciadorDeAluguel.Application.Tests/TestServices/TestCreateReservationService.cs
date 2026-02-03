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

            var savedReservation = await reservationRepo.GetByIdAsync(result);

            Assert.NotNull(savedReservation);
            Assert.Equal(property.Id, savedReservation.Property.Id);
            Assert.Equal(client.Id, savedReservation.Tenant.Id);
        }
    }
}
