using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Tests.InMemoryFakes;
using GerenciadorDeAluguel.Domain.Tests.Builders;
using Xunit;

namespace GerenciadorDeAluguel.Tests.TestServices;

public class TestCheckPropertyAvailabilityService
{
    private readonly InMemoryPropertyRepository _propertyRepository;
    private readonly CheckPropertyAvailabilityService _service;

    public TestCheckPropertyAvailabilityService()
    {
        _propertyRepository = new InMemoryPropertyRepository();
        _service = new CheckPropertyAvailabilityService(_propertyRepository);
    }

    [Fact]
    public async Task ShouldReturnTrueWhenPropertyIsAvailable()
    {
        // Arrange
        var property = TestDataBuilder.CreateValidProperty();
        _propertyRepository.Add(property);

        // Act
        var result = await _service.IsAvailableAsync(property.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenPropertyIsRented()
    {
        // Arrange
        var property = TestDataBuilder.CreateValidProperty();
        // set status to Rented using reflection because setter is private
        var propInfo = property.GetType().GetProperty("Status");
        propInfo!.SetValue(property, PropertyStatus.Rented);
        _propertyRepository.Add(property);

        // Act
        var result = await _service.IsAvailableAsync(property.Id);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ShouldThrowWhenPropertyNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.IsAvailableAsync(nonExistentId)
        );
    }
}
