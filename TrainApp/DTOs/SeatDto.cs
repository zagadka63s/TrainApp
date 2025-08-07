namespace TrainApp.DTOs
{
    public class SeatDto
    {

        public int Id { get; set; }

        public int CoachId { get; set; }

        public int Number {  get; set; }

        public bool IsAvailable { get; set; }
    }
}
