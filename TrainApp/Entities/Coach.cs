namespace TrainApp.Entities
{
    public class Coach
    {

        public int Id { get; set; }

        public int Number { get; set; }

        public string Type { get; set; }

        public int TrainId { get; set; }

        public Train Train { get; set; }

        public ICollection<Seat> Seats { get; set; }
    }
}
