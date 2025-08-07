using System.ComponentModel.DataAnnotations;

namespace TrainApp.DTOs
{
    public class UpdateTrainDto
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
