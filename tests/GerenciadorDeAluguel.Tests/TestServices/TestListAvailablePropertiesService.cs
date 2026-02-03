using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Tests.InMemoryFakes;
using GerenciadorDeAluguel.Tests.Builders;
using Xunit;

namespace GerenciadorDeAluguel.Tests.TestServices;

public class TestListAvailablePropertiesService
{
    private readonly InMemoryPropertyRepository _propertyRepository;
    private readonly ListAvailablePropertiesService _service;

    public TestListAvailablePropertiesService()
    {
        _propertyRepository = new InMemoryPropertyRepository();
        _service = new ListAvailablePropertiesService(_propertyRepository);
    }

    [Fact]
    public async Task ShouldReturnOnlyAvailableProperties()
    {
        // Arrange
        var p1 = TestDataBuilder.CreateValidProperty();
        var p2 = TestDataBuilder.CreateValidProperty();
        var p3 = TestDataBuilder.CreateValidProperty();
        p3.ChangeStatus(PropertyStatus.Rented);

        _propertyRepository.Add(p1);
        _propertyRepository.Add(p2);
        _propertyRepository.Add(p3);

        // Act
        var available = await _service.ListAvailableAsync();

        // Assert
        Assert.Contains(available, p => p.Id == p1.Id);
        Assert.Contains(available, p => p.Id == p2.Id);
        Assert.DoesNotContain(available, p => p.Id == p3.Id);
    }
}
