using Microsoft.AspNetCore.Mvc;
using TrainApp.Services;
using TrainApp.DTOs;

namespace TrainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SeatDto>>> GetAllSeats()
        {
            var seats = await _seatService.GetAllSeatsAsync();
            return Ok(seats);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeatDto>> GetSeatById(int id)
        {
            var seat = await _seatService.GetSeatByIdAsync(id);
            if (seat == null)
                return NotFound();
            return Ok(seat);
        }

        [HttpPost]
        public async Task<ActionResult<SeatDto>> CreateSeat([FromBody] CreateSeatDto dto)
        {
            var created = await _seatService.CreateSeatAsync(dto);
            return CreatedAtAction(nameof(GetSeatById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeat(int id, [FromBody] CreateSeatDto dto)
        {
            var success = await _seatService.UpdateSeatAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            var success = await _seatService.DeleteSeatAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
