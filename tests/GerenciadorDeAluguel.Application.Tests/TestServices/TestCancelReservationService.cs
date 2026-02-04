using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Tests.InMemoryFakes;
using Xunit;
using static GerenciadorDeAluguel.Domain.Tests.Builders.TestDataBuilder;

namespace GerenciadorDeAluguel.Tests.TestServices
{
    public class TestCancelReservationService
    {
        [Fact]
        public async Task CancelReservation_MarksAsCancelled_WhenReservationExists()
        {
            var reservation = CreateValidReservation();
            var repo = new InMemoryReservationRepository();
            await repo.SaveAsync(reservation);
            
            var sut = new CancelReservationService(repo);
            

            await sut.CancelAsync(reservation.Id);

            // Assert
            var cancelledRes = await repo.GetByIdAsync(reservation.Id);
            Assert.NotNull(cancelledRes);
            Assert.True(cancelledRes.IsCancelled);
        }

        [Fact]
        public async Task CancelReservation_ThrowsException_WhenReservationNotFound()
        {
            // Arrange
            var repo = new InMemoryReservationRepository();
            var sut = new CancelReservationService(repo);
            var nonExistentId = Guid.NewGuid();
            
            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await sut.CancelAsync(nonExistentId)
            );
        }

        [Fact]
        public async Task CancelReservation_ThrowsException_WhenAlreadyCancelled()
        {
            // Arrange
            var reservation = CreateValidReservation();
            reservation.Cancel();  // Already cancelled
            
            var repo = new InMemoryReservationRepository();
            await repo.SaveAsync(reservation);
            
            var sut = new CancelReservationService(repo);
            
            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await sut.CancelAsync(reservation.Id)
            );
        }
    }
}
