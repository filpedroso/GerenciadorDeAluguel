using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Domain.ValueObjects;
using GerenciadorDeAluguel.Tests.InMemoryFakes;
using GerenciadorDeAluguel.Tests.Builders;
using Xunit;

namespace GerenciadorDeAluguel.Tests.TestServices;

public class TestRegisterPropertyService
{
    private readonly InMemoryPropertyRepository _propertyRepository;
    private readonly InMemoryClientRepository _clientRepository;
    private readonly RegisterPropertyService _service;

    public TestRegisterPropertyService()
    {
        _propertyRepository = new InMemoryPropertyRepository();
        _clientRepository = new InMemoryClientRepository();
        _service = new RegisterPropertyService(_propertyRepository, _clientRepository);
    }

    [Fact]
    public async Task ShouldRegisterPropertySuccessfully()
    {
        // Arrange
        var owner = TestDataBuilder.CreateValidOwner();
        _clientRepository.Add(owner);

        var dto = new RegisterPropertyDTO
        {
            Type = PropertyType.Apartment,
            Street = "Rua das Flores",
            Number = "123",
            City = "Belo Horizonte",
            State = "MG",
            ZipCode = "30130-100",
            MonthlyRent = 250.00m,
            OwnerId = owner.Id
        };

        // Act
        var propertyId = await _service.ExecuteAsync(dto);

        // Assert
        Assert.NotEqual(Guid.Empty, propertyId);
        var savedProperty = await _propertyRepository.GetByIdAsync(propertyId);
        Assert.NotNull(savedProperty);
        Assert.Equal(PropertyType.Apartment, savedProperty!.Type);
        Assert.Equal(PropertyStatus.Available, savedProperty.Status);

        // Money value assertion
        Assert.Equal(250.00m, savedProperty.MonthlyRent.Value);
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenStreetIsNull()
    {
        // Arrange
        var owner = TestDataBuilder.CreateValidOwner();
        _clientRepository.Add(owner);

        var dto = new RegisterPropertyDTO
        {
            Type = PropertyType.House,
            Street = null!,
            Number = "1",
            City = "Test City",
            State = "TS",
            ZipCode = "12345",
            MonthlyRent = 100m,
            OwnerId = owner.Id
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _service.ExecuteAsync(dto));
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenNumberIsEmpty()
    {
        // Arrange
        var owner = TestDataBuilder.CreateValidOwner();
        _clientRepository.Add(owner);

        var dto = new RegisterPropertyDTO
        {
            Type = PropertyType.House,
            Street = "Test St",
            Number = "",
            City = "Test City",
            State = "TS",
            ZipCode = "12345",
            MonthlyRent = 100m,
            OwnerId = owner.Id
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _service.ExecuteAsync(dto));
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenPriceIsZero()
    {
        // Arrange
        var owner = TestDataBuilder.CreateValidOwner();
        _clientRepository.Add(owner);

        var dto = new RegisterPropertyDTO
        {
            Type = PropertyType.House,
            Street = "Test St",
            Number = "1",
            City = "Test City",
            State = "TS",
            ZipCode = "12345",
            MonthlyRent = 0m,
            OwnerId = owner.Id
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _service.ExecuteAsync(dto));
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenPriceIsNegative()
    {
        // Arrange
        var owner = TestDataBuilder.CreateValidOwner();
        _clientRepository.Add(owner);

        var dto = new RegisterPropertyDTO
        {
            Type = PropertyType.House,
            Street = "Test St",
            Number = "1",
            City = "Test City",
            State = "TS",
            ZipCode = "12345",
            MonthlyRent = -50m,
            OwnerId = owner.Id
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _service.ExecuteAsync(dto));
    }

    [Fact]
    public async Task ShouldSetStatusAsAvailableByDefault()
    {
        // Arrange
        var owner = TestDataBuilder.CreateValidOwner();
        _clientRepository.Add(owner);

        var dto = new RegisterPropertyDTO
        {
            Type = PropertyType.Commercial,
            Street = "Av. Paulista",
            Number = "1000",
            City = "São Paulo",
            State = "SP",
            ZipCode = "01310-100",
            MonthlyRent = 180m,
            OwnerId = owner.Id
        };

        // Act
        var propertyId = await _service.ExecuteAsync(dto);
        var property = await _propertyRepository.GetByIdAsync(propertyId);

        // Assert
        Assert.Equal(PropertyStatus.Available, property!.Status);
        Assert.Equal(180m, property.MonthlyRent.Value);
    }

    [Fact]
    public async Task ShouldCreatePropertyWithCorrectAddressAndPrice()
    {
        // Arrange
        var owner = TestDataBuilder.CreateValidOwner();
        _clientRepository.Add(owner);

        var dto = new RegisterPropertyDTO
        {
            Type = PropertyType.House,
            Street = "Av. Atlântica",
            Number = "500",
            City = "Rio de Janeiro",
            State = "RJ",
            ZipCode = "22070-000",
            MonthlyRent = 450.50m,
            OwnerId = owner.Id
        };

        // Act
        var propertyId = await _service.ExecuteAsync(dto);
        var property = await _propertyRepository.GetByIdAsync(propertyId);

        // Assert
        Assert.Equal("Av. Atlântica", property!.Address.Street);
        Assert.Equal("500", property.Address.Number);
        Assert.Equal("Rio de Janeiro", property.Address.City);
        Assert.Equal(450.50m, property.MonthlyRent.Value);
    }

    [Fact]
    public async Task ShouldThrowWhenOwnerNotFound()
    {
        // Arrange
        var dto = new RegisterPropertyDTO
        {
            Type = PropertyType.Apartment,
            Street = "Rua sem dono",
            Number = "1",
            City = "Cidade",
            State = "ST",
            ZipCode = "00000-000",
            MonthlyRent = 100m,
            OwnerId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.ExecuteAsync(dto));
    }
}
