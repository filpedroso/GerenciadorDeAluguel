using Microsoft.AspNetCore.Mvc;
using GerenciadorDeAluguel.Application.Services;
using GerenciadorDeAluguel.Application.DTOs;

namespace GerenciadorDeAluguel.Api.Controllers;

/// <summary>
/// Manages reservation operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly CreateReservationService _createService;
    private readonly CancelReservationService _cancelService;
    private readonly GetReservationByIdService _getByIdService;
    private readonly ListReservationsService _listService;

    public ReservationsController(
        CreateReservationService createService,
        CancelReservationService cancelService,
        GetReservationByIdService getByIdService,
        ListReservationsService listService)
    {
        _createService = createService;
        _cancelService = cancelService;
        _getByIdService = getByIdService;
        _listService = listService;
    }

    /// <summary>
    /// Create a new reservation
    /// </summary>
    /// <param name="dto">Reservation creation data</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Created reservation ID</returns>
    /// <response code="201">Reservation successfully created</response>
    /// <response code="400">Invalid data, property not available, or entities not found</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateReservationCommandDTO dto,
        CancellationToken ct = default)
    {
        try
        {
            var reservationId = await _createService.CreateAsync(dto, ct);
            return CreatedAtAction(
                nameof(GetById),
                new { id = reservationId },
                new { id = reservationId });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Get reservation by ID
    /// </summary>
    /// <param name="id">Reservation unique identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Reservation data</returns>
    /// <response code="200">Reservation found</response>
    /// <response code="404">Reservation not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
    {
        try
        {
            var reservation = await _getByIdService.GetByIdAsync(id, ct);
            return Ok(reservation);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    /// <summary>
    /// List all reservations
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of reservations</returns>
    /// <response code="200">Returns the list of reservations</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListAll(CancellationToken ct = default)
    {
        var reservations = await _listService.ListAsync(ct);
        return Ok(reservations);
    }

    /// <summary>
    /// Cancel a reservation
    /// </summary>
    /// <param name="id">Reservation unique identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>No content</returns>
    /// <response code="204">Reservation successfully cancelled</response>
    /// <response code="400">Reservation already cancelled</response>
    /// <response code="404">Reservation not found</response>
    [HttpPatch("{id}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken ct = default)
    {
        try
        {
            await _cancelService.CancelAsync(id, ct);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            // Could be "not found" or "already cancelled"
            if (ex.Message.Contains("not found"))
                return NotFound(new { error = ex.Message });
            
            return BadRequest(new { error = ex.Message });
        }
    }
}
