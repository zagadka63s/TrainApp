namespace TrainApp.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int RouteId { get; set; }
        public Route Route { get; set; }

        public int CoachId { get; set; }
        public Coach Coach { get; set; }

        public int SeatId { get; set; }
        public Seat Seat { get; set; }

        public decimal Price { get; set; }
        public DateTime BookingTime { get; set; }
        public string Status { get; set; } // "Booked", "Paid", "Cancelled", "Returned"
        public string PaymentStatus { get; set; } // "Pending", "Success", "Failed"
    }
}
