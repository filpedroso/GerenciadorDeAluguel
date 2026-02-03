using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Application.Services;

public class RegisterClientService
{
    private readonly IClientRepository _clientRepo;

    public RegisterClientService(IClientRepository clientRepo)
    {
        _clientRepo = clientRepo ?? throw new ArgumentNullException(nameof(clientRepo));
    }

    public async Task<Guid> RegisterAsync(RegisterClientDTO dto, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Client name cannot be empty.", nameof(dto.Name));

        if (string.IsNullOrWhiteSpace(dto.DocumentNumber))
            throw new ArgumentException("Client document cannot be empty.", nameof(dto.DocumentNumber));

        Document document;
        try
        {
            document = new Document(dto.DocumentNumber);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Invalid document: {ex.Message}", nameof(dto.DocumentNumber));
        }

        // Check for duplicate document
        var existingClient = await _clientRepo.GetByDocumentAsync(document, ct);
        if (existingClient != null)
            throw new InvalidOperationException($"A client with document '{document.Value}' already exists.");

        Address? address = null;

        bool userProvidedAddress = !string.IsNullOrWhiteSpace(dto.Street) 
                               || !string.IsNullOrWhiteSpace(dto.Number)
                               || !string.IsNullOrWhiteSpace(dto.City)
                               || !string.IsNullOrWhiteSpace(dto.State)
                               || !string.IsNullOrWhiteSpace(dto.ZipCode);

        if (userProvidedAddress)
        {
            // NOW validate all required fields
            if (string.IsNullOrWhiteSpace(dto.Street))
                throw new ArgumentException("Street is required when providing address.", nameof(dto.Street));
            
            if (string.IsNullOrWhiteSpace(dto.Number))
                throw new ArgumentException("Number is required when providing address.", nameof(dto.Number));
            
            if (string.IsNullOrWhiteSpace(dto.City))
                throw new ArgumentException("City is required when providing address.", nameof(dto.City));
            
            if (string.IsNullOrWhiteSpace(dto.State))
                throw new ArgumentException("State is required when providing address.", nameof(dto.State));
            
            if (string.IsNullOrWhiteSpace(dto.ZipCode))
                throw new ArgumentException("ZipCode is required when providing address.", nameof(dto.ZipCode));

            address = new Address(dto.Street, dto.Number, dto.ZipCode, dto.City, dto.State, dto.Complement);
        }

        var client = new Client(
            name: dto.Name,
            email: dto.Email,
            phone: dto.Phone,
            documentNumber: document,
            residentialAddress: address
        );

        await _clientRepo.SaveAsync(client, ct);

        return client.Id;
    }
}
