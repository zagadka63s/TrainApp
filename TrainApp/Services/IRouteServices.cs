using TrainApp.DTOs;

namespace TrainApp.Services
{
    public interface IRouteService
    {
        Task<RouteDto> CreateRouteAsync(CreateRouteDto dto);
        Task<List<RouteDto>> GetAllRoutesAsync();
        Task<RouteDto> GetRouteByIdAsync(int id);
        Task<bool> UpdateRouteAsync(int id, CreateRouteDto dto); // Можно отдельно сделать UpdateRouteDto если логика сложнее
        Task<bool> DeleteRouteAsync(int id);
    }
}
