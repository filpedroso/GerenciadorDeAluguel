using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Tests.InMemoryFakes;
using GerenciadorDeAluguel.Domain.Tests.Builders;
using Xunit;

namespace GerenciadorDeAluguel.Tests.TestServices;

public class TestChangePropertyStatusService
{
    private readonly InMemoryPropertyRepository _propertyRepository;
    private readonly ChangePropertyStatusService _service;

    public TestChangePropertyStatusService()
    {
        _propertyRepository = new InMemoryPropertyRepository();
        _service = new ChangePropertyStatusService(_propertyRepository);
    }

    [Fact]
    public async Task ShouldChangeStatusSuccessfully()
    {
        // Arrange
        var property = TestDataBuilder.CreateValidProperty();
        _propertyRepository.Add(property);

        // Act
        await _service.ChangeStatusAsync(property.Id, PropertyStatus.Rented);

        // Assert
        var saved = await _propertyRepository.GetByIdAsync(property.Id);
        Assert.Equal(PropertyStatus.Rented, saved!.Status);
    }

    [Fact]
    public async Task ShouldThrowWhenPropertyNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.ChangeStatusAsync(nonExistentId, PropertyStatus.Rented)
        );
    }
}
