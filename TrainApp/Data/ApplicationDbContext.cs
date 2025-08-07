using Microsoft.EntityFrameworkCore;
using TrainApp.Entities;
using RouteEntity = TrainApp.Entities.Route;

namespace TrainApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // Таблицы (DbSet) для каждой сущности
        public DbSet<User> Users { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<RouteEntity> Routes { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        // Fluent API — тут все связи, чтобы избежать проблем с каскадами
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User: один ко многим с Ticket
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tickets)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Ограничить каскад

            // Train: один поезд — много вагонов (Coach)
            modelBuilder.Entity<Train>()
                .HasMany(t => t.Coaches)
                .WithOne(c => c.Train)
                .HasForeignKey(c => c.TrainId);

            // Train: один поезд — много маршрутов (Route)
            modelBuilder.Entity<Train>()
                .HasMany(t => t.Routes)
                .WithOne(r => r.Train)
                .HasForeignKey(r => r.TrainId);

            // Coach: один вагон — много мест (Seat)
            modelBuilder.Entity<Coach>()
                .HasMany(c => c.Seats)
                .WithOne(s => s.Coach)
                .HasForeignKey(s => s.CoachId);

            // Route: FromStation и ToStation - оба Restrict, чтобы не было multiple cascade path
            modelBuilder.Entity<RouteEntity>()
                .HasOne(r => r.FromStation)
                .WithMany()
                .HasForeignKey(r => r.FromStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RouteEntity>()
                .HasOne(r => r.ToStation)
                .WithMany()
                .HasForeignKey(r => r.ToStationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Route: один маршрут — много остановок (Stop)
            modelBuilder.Entity<RouteEntity>()
                .HasMany(r => r.Stops)
                .WithOne(s => s.Route)
                .HasForeignKey(s => s.RouteId);

            // Route: один маршрут — много билетов (Ticket)
            modelBuilder.Entity<RouteEntity>()
                .HasMany(r => r.Tickets)
                .WithOne(t => t.Route)
                .HasForeignKey(t => t.RouteId)
                .OnDelete(DeleteBehavior.Restrict); // Ограничить каскад

            // Stop: каждая остановка относится к одной станции
            modelBuilder.Entity<Stop>()
                .HasOne(s => s.Station)
                .WithMany(st => st.Stops)
                .HasForeignKey(s => s.StationId);

            // Ticket: все связи Restrict — чтобы не было multiple cascade paths
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Route)
                .WithMany(r => r.Tickets)
                .HasForeignKey(t => t.RouteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Coach)
                .WithMany()
                .HasForeignKey(t => t.CoachId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany()
                .HasForeignKey(t => t.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket: Price — явно указываем тип decimal (18,2)
            modelBuilder.Entity<Ticket>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
