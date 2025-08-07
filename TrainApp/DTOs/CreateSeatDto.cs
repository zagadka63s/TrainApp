namespace TrainApp.DTOs
{
    public class CreateSeatDto
    {
        public int CoachId { get; set; }      // <-- ID вагона, которому принадлежит место
        public int Number { get; set; }       // <-- Номер места
        public bool IsAvailable { get; set; } // <-- Свободно или занято
    }
}
