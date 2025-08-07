namespace TrainApp.Entities
{
    public class Train
    {

        public int Id { get; set; }
        
        public string Number { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public ICollection<Route> Routes { get; set; }

        public ICollection<Coach> Coaches { get; set; }
    }
}
