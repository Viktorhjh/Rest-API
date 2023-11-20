using System.ComponentModel.DataAnnotations;

namespace Lab_2.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        public string Model { get; set; }
        [Required]
        [MinLength(2)]
        public string Company { get; set; }
        [Required]        
        public double Price { get; set; }

        public int? PersonId{ get; set; }
    }
}
