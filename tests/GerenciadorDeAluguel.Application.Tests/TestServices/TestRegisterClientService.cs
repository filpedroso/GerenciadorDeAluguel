using Xunit;
using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Domain.ValueObjects;
using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Tests.InMemoryFakes;
using GerenciadorDeAluguel.Domain.Tests.Builders;

namespace GerenciadorDeAluguel.Tests.TestServices;

public class TestRegisterClientService
{
    [Fact]
    public async Task RegisterClient_CreatesAndSavesClient_WhenDataIsValid()
    {
        // Arrange
        var repo = new InMemoryClientRepository();
        var sut = new RegisterClientService(repo);
        
        var dto = new RegisterClientDTO
        {
            Name = "João Silva",
            DocumentNumber = TestDataBuilder.ValidDocuments.CPF_1,
            Email = "joao@example.com",
            Phone = "(31) 99999-9999"
        };
        
        // Act
        var clientId = await sut.RegisterAsync(dto);
        
        // Assert
        Assert.NotEqual(Guid.Empty, clientId);
        
        var savedClient = await repo.GetByIdAsync(clientId);
        Assert.NotNull(savedClient);
        Assert.Equal("João Silva", savedClient.Name);
        Assert.Equal(TestDataBuilder.ValidDocuments.CPF_1, savedClient.DocumentNumber.Value);
        Assert.Equal("joao@example.com", savedClient.Email);
        Assert.Null(savedClient.ResidentialAddress);  // No address provided
    }

    [Fact]
    public async Task RegisterClient_CreatesClientWithAddress_WhenAddressProvided()
    {
        // Arrange
        var repo = new InMemoryClientRepository();
        var sut = new RegisterClientService(repo);
        
        var dto = new RegisterClientDTO
        {
            Name = "Maria Santos",
            DocumentNumber = TestDataBuilder.ValidDocuments.CPF_2,
            Email = "maria@example.com",
            Phone = "(31) 88888-8888",
            Street = "Rua das Flores",
            Number = "123",
            City = "Belo Horizonte",
            State = "MG",
            ZipCode = "30000-000"
        };
        
        // Act
        var clientId = await sut.RegisterAsync(dto);
        
        // Assert
        var savedClient = await repo.GetByIdAsync(clientId);
        Assert.NotNull(savedClient);
        Assert.NotNull(savedClient.DocumentNumber.Value);
        Assert.Equal("Rua das Flores", savedClient.ResidentialAddress!.Street);
        Assert.Equal("Belo Horizonte", savedClient.ResidentialAddress.City);
    }

    [Fact]
    public async Task RegisterClient_ThrowsException_WhenDocumentAlreadyExists()
    {
        // Arrange
        var repo = new InMemoryClientRepository();
        var existingClient = TestDataBuilder.CreateValidClient(
            name: "Maria Santos",
            email: "maria@example.com",
            phone: "(31) 88888-8888",
            document: TestDataBuilder.CreateValidCpf(TestDataBuilder.ValidDocuments.CPF_1)
        );
        await repo.SaveAsync(existingClient);
        
        var sut = new RegisterClientService(repo);
        
        var dto = new RegisterClientDTO
        {
            Name = "João Silva",
            DocumentNumber = TestDataBuilder.ValidDocuments.CPF_1,  // Same document
            Email = "joao@example.com",
            Phone = "(31) 99999-9999"
        };
        
        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await sut.RegisterAsync(dto)
        );
    }

    [Fact]
    public async Task RegisterClient_ThrowsException_WhenDocumentIsInvalid()
    {
        // Arrange
        var repo = new InMemoryClientRepository();
        var sut = new RegisterClientService(repo);
        
        var dto = new RegisterClientDTO
        {
            Name = "João Silva",
            DocumentNumber = "123.456.789-00",  // Invalid CPF (fails modulo 11)
            Email = "joao@example.com",
            Phone = "(31) 99999-9999"
        };
        
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await sut.RegisterAsync(dto)
        );
    }

    [Fact]
    public async Task RegisterClient_AcceptsCNPJ_WhenValid()
    {
        // Arrange
        var repo = new InMemoryClientRepository();
        var sut = new RegisterClientService(repo);
        
        var dto = new RegisterClientDTO
        {
            Name = "Empresa LTDA",
            DocumentNumber = TestDataBuilder.ValidDocuments.CNPJ_1,
            Email = "contato@empresa.com",
            Phone = "(31) 3333-3333"
        };
        
        // Act
        var clientId = await sut.RegisterAsync(dto);
        
        // Assert
        var savedClient = await repo.GetByIdAsync(clientId);
        Assert.NotNull(savedClient);
        Assert.Equal(TestDataBuilder.ValidDocuments.CNPJ_1, savedClient.DocumentNumber.Value);
    }

    [Fact]
    public async Task RegisterClient_ThrowsException_WhenNameIsEmpty()
    {
        // Arrange
        var repo = new InMemoryClientRepository();
        var sut = new RegisterClientService(repo);
        
        var dto = new RegisterClientDTO
        {
            Name = "",  // Invalid
            DocumentNumber = TestDataBuilder.ValidDocuments.CPF_1,
            Email = "joao@example.com",
            Phone = "(31) 99999-9999"
        };
        
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await sut.RegisterAsync(dto)
        );
    }

    [Fact]
    public async Task RegisterClient_ThrowsException_WhenDocumentNumberIsEmpty()
    {
        // Arrange
        var repo = new InMemoryClientRepository();
        var sut = new RegisterClientService(repo);
        
        var dto = new RegisterClientDTO
        {
            Name = "João Silva",
            DocumentNumber = "",  // Invalid
            Email = "joao@example.com",
            Phone = "(31) 99999-9999"
        };
        
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await sut.RegisterAsync(dto)
        );
    }
}
