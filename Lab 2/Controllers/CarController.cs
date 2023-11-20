using Lab_2.Authentication;
using Lab_2.Models;
using Lab_2.Reposetory;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lab_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class CarController : ControllerBase
    {
        static CarReposetory reposetory = new CarReposetory();
        static PersonReposetory personReposetory = new PersonReposetory();
        private DataService dataService;

        public CarController(DataService _dataService)
        {
            dataService = _dataService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            dataService.Cars = reposetory.loadData();
            return Ok(dataService.Cars);
        }

        [HttpGet("{id}")]        
        public ActionResult<IEnumerable<Person>> Get(int id)
        {
            dataService.Cars = reposetory.loadData();
            bool carExists = dataService.Cars.Any(car => car.Id == id);
            if (carExists)
            {
                Car car = dataService.Cars.Find(car => car.Id == id);              
                return Ok(car);
            }
            return NoContent();
        }

        [HttpDelete, BasicAuthorization]
        public IActionResult Remove(int id)
        {
            dataService.Cars = reposetory.loadData();
            dataService.Persons = personReposetory.loadData();

            Car car = dataService.Cars.Find(c => c.Id == id);
            if (car == null)
                return NotFound();

            if (car.PersonId != null)
            {
                Person person = dataService.Persons.Find(a => a.Id == car.PersonId);
                if (person != null)
                {
                    person.CarId = null;
                }
            }

            dataService.Cars.Remove(car);

            personReposetory.saveData(dataService.Persons);
            reposetory.saveData(dataService.Cars);

            return NoContent();
        }

        [HttpPost, BasicAuthorization]
        public IActionResult Post(Car car)
        {            
            car.Id = dataService.Cars.Count;            

            if (car.PersonId != null)
            {
                Person person = dataService.Persons.Find(c => c.Id == car.PersonId);

                if (person == null) 
                    car.PersonId = null;
                else 
                    person.CarId = car.Id;
            }

            personReposetory.saveData(dataService.Persons);
            reposetory.saveData(dataService.Cars);

            return Ok(car);
        }

        [HttpPut, BasicAuthorization]
        public IActionResult Put(Car car)
        {
            if (car == null)
            {
                return NoContent();
            }
            int index = dataService.Cars.FindIndex(c => c.Id == car.Id);
            if (index == -1)
            {
                return NoContent();
            }
            
            Remove(car.Id);            
            Post(car);

            return Ok();
        }
    }
}
