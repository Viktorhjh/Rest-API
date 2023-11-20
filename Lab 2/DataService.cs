using Lab_2.Models;
using Lab_2.Reposetory;

namespace Lab_2
{
    public class DataService
    {
        public List<Person> Persons { get; set; }
        public List<Car> Cars { get; set; }        

        public DataService() {
            PersonReposetory personReposetory = new PersonReposetory();
            CarReposetory carReposetory = new CarReposetory();

            Persons = new List<Person>();
            Cars = new List<Car>();

            Persons = personReposetory.loadData();
            Cars = carReposetory.loadData();
        }


    }
}
