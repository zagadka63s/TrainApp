using TrainApp.Data;
using TrainApp.DTOs;
using TrainApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace TrainApp.Services
{
    public class TrainService : ITrainService
    {
        private readonly ApplicationDbContext _context;

        // Внедряем ApplicationDbContext через конструктор
        public TrainService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Реализация CreateTrainAsync
        public async Task<TrainDto> CreateTrainAsync(CreateTrainDto dto)
        {
            var train = new Train
            {
                Number = dto.Number,
                Name = dto.Name,
                Type = dto.Type
            };

            _context.Trains.Add(train);
            await _context.SaveChangesAsync();

            // Возвращаем DTO с Id и введёнными данными
            return new TrainDto
            {
                Id = train.Id,
                Number = train.Number,
                Name = train.Name,
                Type = train.Type
            };
        }

        // Получить все поезда (список)
        public async Task<List<TrainDto>> GetAllTrainsAsync()
        {
            return await _context.Trains
                .Select(t => new TrainDto
                {
                    Id = t.Id,
                    Number = t.Number,
                    Name = t.Name,
                    Type = t.Type
                })
                .ToListAsync();
        }

        // Получить поезд по Id (детально)
        public async Task<TrainDetailsDto> GetTrainByIdAsync(int id)
        {
            var train = await _context.Trains
                .FirstOrDefaultAsync(t => t.Id == id);

            if (train == null) return null;

            return new TrainDetailsDto
            {
                Id = train.Id,
                Number = train.Number,
                Name = train.Name,
                Type = train.Type
                // Сюда позже можно добавить вложенные списки вагонов или маршрутов
            };
        }

        // Обновить поезд по Id
        public async Task<bool> UpdateTrainAsync(int id, UpdateTrainDto dto)
        {
            var train = await _context.Trains.FindAsync(id);
            if (train == null) return false;

            train.Number = dto.Number;
            train.Name = dto.Name;
            train.Type = dto.Type;

            await _context.SaveChangesAsync();
            return true;
        }

        // Удалить поезд по Id
        public async Task<bool> DeleteTrainAsync(int id)
        {
            var train = await _context.Trains.FindAsync(id);
            if (train == null) return false;

            _context.Trains.Remove(train);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
