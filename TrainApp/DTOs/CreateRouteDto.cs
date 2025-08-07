namespace TrainApp.DTOs
{
    public class CreateRouteDto
    {
        public int TrainId { get; set; }
        public int FromStationId { get; set; }
        public int ToStationId { get; set; }
        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }


    }
}
