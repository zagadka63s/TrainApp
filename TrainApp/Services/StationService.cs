using TrainApp.Data;
using TrainApp.DTOs;
using TrainApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace TrainApp.Services
{
    public class StationService : IStationService
    {
        private readonly ApplicationDbContext _context;

        public StationService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Создать станцию
        public async Task<StationDto> CreateStationAsync(CreateStationDto dto)
        {
            var station = new Station
            {
                Name = dto.Name,
                City = dto.City,
                Code = dto.Code
            };

            _context.Stations.Add(station);
            await _context.SaveChangesAsync();

            return new StationDto
            {
                Id = station.Id,
                Name = station.Name,
                City = station.City,
                Code = station.Code
            };
        }

        // Получить все станции
        public async Task<List<StationDto>> GetAllStationsAsync()
        {
            return await _context.Stations
                .Select(s => new StationDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    City = s.City,
                    Code = s.Code
                })
                .ToListAsync();
        }

        // Получить станцию по Id
        public async Task<StationDto> GetStationByIdAsync(int id)
        {
            var station = await _context.Stations.FirstOrDefaultAsync(s => s.Id == id);
            if (station == null) return null;

            return new StationDto
            {
                Id = station.Id,
                Name = station.Name,
                City = station.City,
                Code = station.Code
            };
        }

        // Обновить станцию по Id
        public async Task<bool> UpdateStationAsync(int id, CreateStationDto dto)
        {
            var station = await _context.Stations.FindAsync(id);
            if (station == null) return false;

            station.Name = dto.Name;
            station.City = dto.City;
            station.Code = dto.Code;

            await _context.SaveChangesAsync();
            return true;
        }

        // Удалить станцию по Id
        public async Task<bool> DeleteStationAsync(int id)
        {
            var station = await _context.Stations.FindAsync(id);
            if (station == null) return false;

            _context.Stations.Remove(station);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
