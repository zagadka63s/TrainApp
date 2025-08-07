using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainApp.Services;
using TrainApp.DTOs;

namespace TrainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly ITrainService _trainService;

        public TrainsController(ITrainService trainService)
        {
            _trainService = trainService;
        }

        /// <summary>
        /// Get list all trains
        /// </summary>
        /// <returns>List trains </returns>
        [HttpGet]
        public async Task<ActionResult<List<TrainDto>>> GetAllTrains()
        {
            var trains = await _trainService.GetAllTrainsAsync();
            return Ok(trains);
        }

        /// <summary>
        /// Get detailed information about train by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainDetailsDto>> GetTrainById(int id)
        {
            var train = await _trainService.GetTrainByIdAsync(id);
            if(train == null)
                return NotFound();

            return Ok(train);
        }

        /// <summary>
        /// Create new train
        /// </summary> 
        [HttpPost]
        public async Task<ActionResult<TrainDto>> CreateTrain([FromBody] CreateTrainDto dto)
        {
            var created = await _trainService.CreateTrainAsync(dto);
            return CreatedAtAction(nameof(GetTrainById), new { id= created.Id }, created);
        }

        /// <summary>
        /// Update train by Id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrain(int id, [FromBody] UpdateTrainDto dto)
        {
            var success = await _trainService.UpdateTrainAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Delete train by Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrain(int id)
        {
            var success = await _trainService.DeleteTrainAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }


    }
}
