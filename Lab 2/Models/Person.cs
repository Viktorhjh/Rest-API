using System.ComponentModel.DataAnnotations;

namespace Lab_2.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        public string Surname { get; set; }
        public string City { get; set; }
        [Required]
        [Range(0, 99)]
        public int Age { get; set; }
        public int? CarId { get; set; }
    }
}
