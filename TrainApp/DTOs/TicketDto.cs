namespace TrainApp.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime BookingTime { get; set; }
        public string Status { get; set; }
        public string TrainNumber { get; set; }
        public string TrainName { get; set; }
        public string CoachType { get; set; }
        public int CoachNumber { get; set; }
        public int SeatNumber { get; set; }
        public string FromStation { get; set; }
        public string ToStation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }


}
