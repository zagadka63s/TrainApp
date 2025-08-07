namespace TrainApp.Entities
{
    public class Stop
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }

        public int StationId { get; set; }
        public Station Station { get; set; }

        public int StopOrder { get; set; } // Порядок следования
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
