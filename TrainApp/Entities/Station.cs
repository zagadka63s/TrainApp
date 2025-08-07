namespace TrainApp.Entities
{
    public class Station
    {

        public int Id { get; set; }
        public string Name { get; set; }
        
        public string City { get; set; }

        public string Code { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public ICollection<Stop> Stops { get; set; }
    }
}
