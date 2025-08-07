using Microsoft.AspNetCore.Mvc;
using TrainApp.Services;
using TrainApp.DTOs;

namespace TrainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly IStationService _stationService;

        // Сервис внедряется через конструктор (Dependency Injection)
        public StationsController(IStationService stationService)
        {
            _stationService = stationService;
        }

        /// <summary>
        /// Получить список всех станций
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<StationDto>>> GetAllStations()
        {
            var stations = await _stationService.GetAllStationsAsync();
            return Ok(stations);
        }

        /// <summary>
        /// Получить станцию по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<StationDto>> GetStationById(int id)
        {
            var station = await _stationService.GetStationByIdAsync(id);
            if (station == null)
                return NotFound();
            return Ok(station);
        }

        /// <summary>
        /// Создать новую станцию
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<StationDto>> CreateStation([FromBody] CreateStationDto dto)
        {
            var created = await _stationService.CreateStationAsync(dto);
            return CreatedAtAction(nameof(GetStationById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Обновить станцию по Id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStation(int id, [FromBody] CreateStationDto dto)
        {
            var success = await _stationService.UpdateStationAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Удалить станцию по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            var success = await _stationService.DeleteStationAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
