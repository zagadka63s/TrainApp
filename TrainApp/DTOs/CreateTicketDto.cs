using System.ComponentModel.DataAnnotations;

namespace TrainApp.DTOs
{
    public class CreateTicketDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RouteId { get; set; }

        [Required]

        public int CoachId { get; set; }

        [Required]

        public int SeatId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]

        public DateTime BookingTime { get; set; }

        public string Status { get; set; }

        
    }
}
