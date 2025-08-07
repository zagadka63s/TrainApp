using Microsoft.AspNetCore.Mvc;
using TrainApp.DTOs;
using TrainApp.Services;


namespace TrainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {

        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }
        ///<summary>
        ///Get list all routes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<RouteDto>>> GetAllRoutes()
        {
            var toutes = await _routeService.GetAllRoutesAsync();
            return Ok(toutes);
        }

        ///<summary>
        /// Get route by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDto>> GetRouteById(int id)
        {
            var route = await _routeService.GetRouteByIdAsync(id);
            if (route == null)
                return NotFound();
            return Ok(route);
        }

        /// <summary>
        /// Create route
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RouteDto>> CreateRoute([FromBody] CreateRouteDto dto)
        {
            var created = await _routeService.CreateRouteAsync(dto);
            return CreatedAtAction(nameof(GetRouteById), new { id = created.Id }, created);
        }

        ///<summary>
        ///Update route by id 
        /// </summary>
        /// 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoute(int id, [FromBody] CreateRouteDto dto)
        {
            var success = await _routeService.UpdateRouteAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Удалить маршрут по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var success = await _routeService.DeleteRouteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }


    }

}
