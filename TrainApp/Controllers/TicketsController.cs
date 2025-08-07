using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainApp.DTOs;
using TrainApp.Services;

namespace TrainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        ///<summary>
        ///Get list all tickets ( for admin )
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<TicketDto>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        /// <summary>
        /// Get ticket by Id
        /// </summary>
        [HttpGet("{id}")]

        public async Task<ActionResult<TicketDto>> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound();
            return Ok(ticket);
            
        }

        /// <summary>
        /// Create new ticket ( reservation / buy )
        /// </summary>
        [HttpPost]

        public async Task<IActionResult> UpdateTicket(int id, [FromBody] CreateTicketDto dto)
        {
            var success = await _ticketService.UpdateTicketAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete ticket by Id ( canceled reservation / buy )
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var success = await _ticketService.DeleteTicketAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
            
        }
        
    }
}
