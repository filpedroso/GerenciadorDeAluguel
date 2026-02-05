using Microsoft.AspNetCore.Mvc;
using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Api.Controllers;

/// <summary>
/// Manages client operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly RegisterClientService _registerService;
    private readonly ListClientsService _listService;
    private readonly IClientRepository _repository;

    public ClientsController(
        RegisterClientService registerService,
        ListClientsService listService,
        IClientRepository repository)
    {
        _registerService = registerService;
        _listService = listService;
        _repository = repository;
    }

    /// <summary>
    /// List all registered clients
    /// </summary>
    /// <returns>List of clients</returns>
    /// <response code="200">List of clients returned successfully</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var clients = await _listService.GetAllAsync(ct);
        return Ok(clients);
    }

    /// <summary>
    /// Register a new client
    /// </summary>
    /// <param name="dto">Client registration data</param>
    /// <returns>Created client</returns>
    /// <response code="201">Client successfully registered</response>
    /// <response code="400">Invalid data or duplicate document</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterClientDTO dto)
    {
        try
        {
            var client = await _registerService.RegisterAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = client }, client);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Get client by ID
    /// </summary>
    /// <param name="id">Client unique identifier</param>
    /// <returns>Client data</returns>
    /// <response code="200">Client found</response>
    /// <response code="404">Client not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var client = await _repository.GetByIdAsync(id);
        
        if (client == null)
            return NotFound(new { error = "Client not found" });
        
        return Ok(client);
    }

    /// <summary>
    /// Get client by document (CPF/CNPJ)
    /// </summary>
    /// <param name="documentNumber">CPF or CNPJ number</param>
    /// <returns>Client data</returns>
    /// <response code="200">Client found</response>
    /// <response code="404">Client not found</response>
    /// <response code="400">Invalid document number</response>
    [HttpGet("document/{documentNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByDocument(string documentNumber)
    {
        try
        {
            // Convert string → Domain VO
            var document = new Document(documentNumber);
            
            var client = await _repository.GetByDocumentAsync(document);
            
            if (client == null)
                return NotFound(new { error = "Client not found" });
            
            return Ok(client);
        }
        catch (ArgumentException ex)
        {
            // Document validation failed
            return BadRequest(new { error = ex.Message });
        }
    }
}
