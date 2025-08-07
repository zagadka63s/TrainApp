using Microsoft.AspNetCore.Mvc;
using TrainApp.Services;
using TrainApp.DTOs;

namespace TrainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {
        private readonly ICoachService _coachService;

        public CoachesController(ICoachService coachService)
        {
            _coachService = coachService;
        }

        /// <summary>
        /// Получить список всех вагонов
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<CoachDto>>> GetAllCoaches()
        {
            var coaches = await _coachService.GetAllCoachesAsync();
            return Ok(coaches);
        }

        /// <summary>
        /// Получить вагон по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CoachDto>> GetCoachById(int id)
        {
            var coach = await _coachService.GetCoachByIdAsync(id);
            if (coach == null)
                return NotFound();
            return Ok(coach);
        }

        /// <summary>
        /// Создать новый вагон
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CoachDto>> CreateCoach([FromBody] CreateCoachDto dto)
        {
            var created = await _coachService.CreateCoachAsync(dto);
            return CreatedAtAction(nameof(GetCoachById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Обновить вагон по Id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoach(int id, [FromBody] CreateCoachDto dto)
        {
            var success = await _coachService.UpdateCoachAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Удалить вагон по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            var success = await _coachService.DeleteCoachAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
