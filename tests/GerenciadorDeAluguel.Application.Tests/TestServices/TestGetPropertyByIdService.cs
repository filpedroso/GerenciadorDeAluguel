using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Tests.InMemoryFakes;
using GerenciadorDeAluguel.Domain.Tests.Builders;
using Xunit;

namespace GerenciadorDeAluguel.Tests.TestServices;

public class TestGetPropertyByIdService
{
    private readonly InMemoryPropertyRepository _propertyRepository;
    private readonly GetPropertyByIdService _service;

    public TestGetPropertyByIdService()
    {
        _propertyRepository = new InMemoryPropertyRepository();
        _service = new GetPropertyByIdService(_propertyRepository);
    }

    [Fact]
    public async Task ShouldReturnPropertyWhenExists()
    {
        // Arrange
        var property = TestDataBuilder.CreateValidProperty();
        _propertyRepository.Add(property);

        // Act
        var result = await _service.GetByIdAsync(property.Id);

        // Assert
        Assert.Equal(property, result);
    }

    [Fact]
    public async Task ShouldThrowWhenNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.GetByIdAsync(id));
    }
}
