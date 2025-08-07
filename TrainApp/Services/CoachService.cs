using TrainApp.Data;
using TrainApp.DTOs;
using TrainApp.Entities;
using Microsoft.EntityFrameworkCore;


namespace TrainApp.Services
{
    public class CoachService : ICoachService
    {
        private readonly ApplicationDbContext _context;

        public CoachService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create new coach
        /// </summary>
        public async Task<CoachDto> CreateCoachAsync(CreateCoachDto dto)
        {
            var trainExists = await _context.Trains.AnyAsync(t => t.Id == dto.TrainId);
            if (!trainExists)
                throw new ArgumentException("The specified train was not found.");

            var coach = new Coach
            {
                TrainId = dto.TrainId,
                Type = dto.Type,
                Number = dto.Number,
            };

            _context.Coaches.Add(coach);
            await _context.SaveChangesAsync();

            return new CoachDto
            {
                Id = coach.Id,
                TraindId = coach.TrainId,
                Type = coach.Type,
                Number = coach.Number,
            };
        }

        /// <summary>
        /// Get list all coach
        /// </summary>
        public async Task<List<CoachDto>> GetAllCoachesAsync()
        {
            return await _context.Coaches
                .Select(c => new CoachDto
                {
                    Id = c.Id,
                    TraindId= c.TrainId,
                    Type = c.Type,
                    Number= c.Number,
                })
                .ToListAsync();
        }

        /// <summary>
        /// Get wagon by id
        /// </summary>
        public async Task<CoachDto> GetCoachByIdAsync(int id)
        {
            var coach = await _context.Coaches.FirstOrDefaultAsync(c => c.Id == id);
            if (coach == null) return null;

            return new CoachDto()
            {
                Id = coach.Id,
                TraindId = coach.TrainId,
                Type = coach.Type,
                Number = coach.Number,
            };
        }

        /// <summary>
        /// Update information coach by id
        /// </summary>
        public async Task<bool> UpdateCoachAsync(int id, CreateCoachDto dto)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null) return false;

            if (!await _context.Trains.AnyAsync(t => t.Id == dto.TrainId))
                throw new ArgumentException("The specified train was not found");

            coach.TrainId = dto.TrainId;
            coach.Type = dto.Type;
            coach.Number = dto.Number;

            await _context.SaveChangesAsync();
            return true;   
        }

        /// <summary>
        /// Delete coach by Id
        /// </summary>
        public async Task<bool> DeleteCoachAsync(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null) return false;

            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
