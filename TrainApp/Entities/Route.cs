namespace TrainApp.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public Train Train { get; set; }

        public int FromStationId { get; set; }
        public Station FromStation { get; set; }

        public int ToStationId { get; set; }
        public Station ToStation { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        // Список всех остановок на маршруте
        public ICollection<Stop> Stops { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
