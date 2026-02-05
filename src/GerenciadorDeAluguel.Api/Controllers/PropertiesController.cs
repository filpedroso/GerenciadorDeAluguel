using Microsoft.AspNetCore.Mvc;
using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Domain.Enums;

namespace GerenciadorDeAluguel.Api.Controllers;

/// <summary>
/// Provides endpoints for managing properties in the rental system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly RegisterPropertyService _registerService;
    private readonly GetPropertyByIdService _getByIdService;
    private readonly ListAvailablePropertiesService _listAvailableService;
    private readonly ChangePropertyStatusService _changeStatusService;
    private readonly CheckPropertyAvailabilityService _checkAvailabilityService;

    public PropertiesController(
        RegisterPropertyService registerService,
        GetPropertyByIdService getByIdService,
        ListAvailablePropertiesService listAvailableService,
        ChangePropertyStatusService changeStatusService,
        CheckPropertyAvailabilityService checkAvailabilityService)
    {
        _registerService = registerService;
        _getByIdService = getByIdService;
        _listAvailableService = listAvailableService;
        _changeStatusService = changeStatusService;
        _checkAvailabilityService = checkAvailabilityService;
    }

    /// <summary>
    /// Registers a new property in the rental management system.
    /// </summary>
    /// <param name="dto">The data required to register the property.</param>
    /// <param name="ct">Cancellation token to cancel the operation.</param>
    /// <returns>The unique identifier of the created property.</returns>
    /// <response code="201">Property successfully registered.</response>
    /// <response code="400">Invalid data provided or owner not found.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterPropertyDTO dto,
        CancellationToken ct = default)
    {
        try
        {
            var propertyId = await _registerService.ExecuteAsync(dto, ct);
            return CreatedAtAction(
                nameof(GetById),
                new { id = propertyId },
                new { id = propertyId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves a property by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the property.</param>
    /// <param name="ct">Cancellation token to cancel the operation.</param>
    /// <returns>The property data.</returns>
    /// <response code="200">Property found.</response>
    /// <response code="404">Property not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
    {
        try
        {
            var property = await _getByIdService.GetByIdAsync(id, ct);
            return Ok(property);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves a list of all available properties.
    /// </summary>
    /// <param name="ct">Cancellation token to cancel the operation.</param>
    /// <returns>A list of available properties.</returns>
    /// <response code="200">Returns the list of available properties.</response>
    [HttpGet("available")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListAvailable(CancellationToken ct = default)
    {
        var properties = await _listAvailableService.ListAvailableAsync(ct);
        return Ok(properties);
    }

    /// <summary>
    /// Changes the status of a property.
    /// </summary>
    /// <param name="id">The unique identifier of the property.</param>
    /// <param name="dto">The new status for the property.</param>
    /// <param name="ct">Cancellation token to cancel the operation.</param>
    /// <returns>No content.</returns>
    /// <response code="204">Status successfully changed.</response>
    /// <response code="400">Invalid status value.</response>
    /// <response code="404">Property not found.</response>
    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeStatus(
        Guid id,
        [FromBody] ChangePropertyStatusDTO dto,
        CancellationToken ct = default)
    {
        try
        {
            await _changeStatusService.ChangeStatusAsync(id, dto.NewStatus, ct);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Checks if a property is available for rental.
    /// </summary>
    /// <param name="id">The unique identifier of the property.</param>
    /// <param name="ct">Cancellation token to cancel the operation.</param>
    /// <returns>The availability status of the property.</returns>
    /// <response code="200">Returns availability status.</response>
    /// <response code="404">Property not found.</response>
    [HttpGet("{id}/availability")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CheckAvailability(Guid id, CancellationToken ct = default)
    {
        try
        {
            var isAvailable = await _checkAvailabilityService.IsAvailableAsync(id, ct);
            return Ok(new { propertyId = id, isAvailable });
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}
