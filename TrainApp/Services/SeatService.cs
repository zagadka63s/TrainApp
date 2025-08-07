using TrainApp.Data;
using TrainApp.DTOs;
using TrainApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace TrainApp.Services
{
    public class SeatService : ISeatService
    {
        private readonly ApplicationDbContext _context;

        public SeatService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Создать новое место
        public async Task<SeatDto> CreateSeatAsync(CreateSeatDto dto)
        {
            // Можно проверить существование Coach
            var coachExists = await _context.Coaches.AnyAsync(c => c.Id == dto.CoachId);
            if (!coachExists)
                throw new ArgumentException("Указанный вагон не найден");

            var seat = new Seat
            {
                CoachId = dto.CoachId,
                Number = dto.Number,
                IsAvailable = dto.IsAvailable
            };

            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();

            return new SeatDto
            {
                Id = seat.Id,
                CoachId = seat.CoachId,
                Number = seat.Number,
                IsAvailable = seat.IsAvailable
            };
        }

        // Получить все места
        public async Task<List<SeatDto>> GetAllSeatsAsync()
        {
            return await _context.Seats
                .Select(s => new SeatDto
                {
                    Id = s.Id,
                    CoachId = s.CoachId,
                    Number = s.Number,
                    IsAvailable = s.IsAvailable
                })
                .ToListAsync();
        }

        // Получить место по Id
        public async Task<SeatDto> GetSeatByIdAsync(int id)
        {
            var seat = await _context.Seats.FirstOrDefaultAsync(s => s.Id == id);
            if (seat == null) return null;

            return new SeatDto
            {
                Id = seat.Id,
                CoachId = seat.CoachId,
                Number = seat.Number,
                IsAvailable = seat.IsAvailable
            };
        }

        // Обновить место
        public async Task<bool> UpdateSeatAsync(int id, CreateSeatDto dto)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null) return false;

            // Можно проверить существование Coach
            if (!await _context.Coaches.AnyAsync(c => c.Id == dto.CoachId))
                throw new ArgumentException("Указанный вагон не найден");

            seat.CoachId = dto.CoachId;
            seat.Number = dto.Number;
            seat.IsAvailable = dto.IsAvailable;

            await _context.SaveChangesAsync();
            return true;
        }

        // Удалить место
        public async Task<bool> DeleteSeatAsync(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null) return false;

            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
