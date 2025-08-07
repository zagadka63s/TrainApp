using TrainApp.Data;
using TrainApp.DTOs;
using TrainApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace TrainApp.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a new ticket
        /// </summary>

        public async Task<TicketDto> CreateTicketAsync(CreateTicketDto dto)
        {
            var ticket = new Ticket
            {
                UserId = dto.UserId,
                RouteId = dto.RouteId,
                CoachId = dto.CoachId,
                SeatId = dto.SeatId,
                Price = dto.Price,
                BookingTime = dto.BookingTime,
                Status = dto.Status,
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            var t = await _context.Tickets
                .Include(ti => ti.Route)
                    .ThenInclude(r => r.Train)
                .Include(ti => ti.Route)
                    .ThenInclude(r => r.FromStation)
                .Include(ti => ti.Route)
                    .ThenInclude(r => r.ToStation)
                .Include(ti => ti.Coach)
                .Include(ti => ti.Seat)
                .FirstOrDefaultAsync(ti => ti.Id == ticket.Id);

            return new TicketDto
            {
                Id = t.Id,
                Price = t.Price,
                BookingTime = t.BookingTime,
                Status = t.Status,
                TrainNumber = t.Route.Train.Name,
                CoachType = t.Coach.Type,
                CoachNumber = t.Coach.Number,
                SeatNumber = t.Seat.Number,
                FromStation = t.Route.FromStation.Name,
                DepartureTime = t.Route.DepartureTime,
                ArrivalTime = t.Route.ArrivalTime
            };
        }

        ///<summary>
        ///Get list all tickets
        /// </summary>

        

        ///<summary>
        ///Get ticket by Id 
        /// </summary>
        public async Task<TicketDto> GetTicketByIdAsync(int id)
        {
            var t = await _context.Tickets
                .Include(ti => ti.Route)
                    .ThenInclude(r => r.Train)
                .Include(ti => ti.Route)
                    .ThenInclude(r => r.FromStation)
                .Include(ti => ti.Route)
                    .ThenInclude(r => r.ToStation)
                .Include(ti => ti.Coach)
                .Include(ti => ti.Seat)
                .FirstOrDefaultAsync(ti => ti.Id == id);

            if (t == null) return null;

            return new TicketDto
            {
                Id = t.Id,
                Price = t.Price,
                BookingTime = t.BookingTime,
                Status = t.Status,
                TrainNumber = t.Route.Train.Number,
                CoachType = t.Coach.Type,
                CoachNumber = t.Coach.Number,
                SeatNumber = t.Seat.Number,
                FromStation = t.Route.FromStation.Name,
                DepartureTime = t.Route.DepartureTime,
                ArrivalTime = t.Route.ArrivalTime,
            };

        }

        ///<summary>
        ///Update ticket by id
        /// </summary>

        public async Task<bool> UpdateTicketAsync(int id, CreateTicketDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return false;

            ticket.UserId = dto.UserId;
            ticket.RouteId = dto.RouteId;
            ticket.CoachId = dto.CoachId;
            ticket.SeatId = dto.SeatId;
            ticket.Price = dto.Price;
            ticket.BookingTime = dto.BookingTime;
            ticket.Status = dto.Status;

            await _context.SaveChangesAsync();
            return true;

        }

        ///<summary>
        ///Delete ticket by id
        /// </summary>
        public async Task<bool> DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return false;

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TicketDto>> GetAllTicketsAsync()
        {
            return await _context.Tickets
                .Include(t => t.Route)
                    .ThenInclude(r => r.Train)
                .Include(t => t.Route)
                    .ThenInclude(r => r.FromStation)
                .Include(t => t.Route)
                    .ThenInclude(r => r.ToStation)
                .Include(t => t.Coach)
                .Include(t => t.Seat)
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    Price = t.Price,
                    BookingTime = t.BookingTime,
                    Status = t.Status,
                    TrainNumber = t.Route.Train.Number,
                    TrainName = t.Route.Train.Name,
                    CoachType = t.Coach.Type,
                    CoachNumber = t.Coach.Number,
                    SeatNumber = t.Seat.Number,
                    FromStation = t.Route.FromStation.Name,
                    ToStation = t.Route.ToStation.Name,
                    DepartureTime = t.Route.DepartureTime,
                    ArrivalTime = t.Route.ArrivalTime,
                })
                .ToListAsync();

        }

    }



}
