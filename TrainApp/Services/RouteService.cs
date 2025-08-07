using TrainApp.Data;
using TrainApp.DTOs;
using TrainApp.Entities;
using Microsoft.EntityFrameworkCore;
using RouteEntity = TrainApp.Entities.Route;

namespace TrainApp.Services
{
    public class RouteService : IRouteService
    {
        private readonly ApplicationDbContext _context;

        public RouteService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Создать маршрут
        public async Task<RouteDto> CreateRouteAsync(CreateRouteDto dto)
        {
            var route = new RouteEntity
            {
                TrainId = dto.TrainId,
                FromStationId = dto.FromStationId,
                ToStationId = dto.ToStationId,
                DepartureTime = dto.DepartureTime,
                ArrivalTime = dto.ArrivalTime
            };

            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            return new RouteDto
            {
                Id = route.Id,
                TrainId = route.TrainId,
                FromStationId = route.FromStationId,
                ToStationId = route.ToStationId,
                DepartureTime = route.DepartureTime,
                ArrivalTime = route.ArrivalTime
            };
        }

        // Получить все маршруты
        public async Task<List<RouteDto>> GetAllRoutesAsync()
        {
            return await _context.Routes
                .Select(r => new RouteDto
                {
                    Id = r.Id,
                    TrainId = r.TrainId,
                    FromStationId = r.FromStationId,
                    ToStationId = r.ToStationId,
                    DepartureTime = r.DepartureTime,
                    ArrivalTime = r.ArrivalTime
                })
                .ToListAsync();
        }

        // Получить маршрут по Id
        public async Task<RouteDto> GetRouteByIdAsync(int id)
        {
            var route = await _context.Routes.FirstOrDefaultAsync(r => r.Id == id);
            if (route == null) return null;

            return new RouteDto
            {
                Id = route.Id,
                TrainId = route.TrainId,
                FromStationId = route.FromStationId,
                ToStationId = route.ToStationId,
                DepartureTime = route.DepartureTime,
                ArrivalTime = route.ArrivalTime
            };
        }

        // Обновить маршрут
        public async Task<bool> UpdateRouteAsync(int id, CreateRouteDto dto)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null) return false;

            route.TrainId = dto.TrainId;
            route.FromStationId = dto.FromStationId;
            route.ToStationId = dto.ToStationId;
            route.DepartureTime = dto.DepartureTime;
            route.ArrivalTime = dto.ArrivalTime;

            await _context.SaveChangesAsync();
            return true;
        }

        // Удалить маршрут
        public async Task<bool> DeleteRouteAsync(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null) return false;

            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
