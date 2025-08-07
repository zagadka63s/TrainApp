using TrainApp.Entities;

public class Seat
{
    public int Id { get; set; }
    public int CoachId { get; set; }
    public Coach Coach { get; set; }
    public int Number { get; set; }
    public bool IsAvailable { get; set; }
}
