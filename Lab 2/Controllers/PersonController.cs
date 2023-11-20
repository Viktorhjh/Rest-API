using Lab_2.Authentication;
using Lab_2.Models;
using Lab_2.Reposetory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lab_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class PersonController : ControllerBase
    {        
        static PersonReposetory reposetory = new PersonReposetory();
        static CarReposetory carReposetory = new CarReposetory();
        private DataService dataService;

        public PersonController(DataService _dataService)
        {
            dataService = _dataService;
        }

        [HttpGet]        
        public ActionResult<IEnumerable<Person>> Get()
        {
            dataService.Persons = reposetory.loadData();
            return Ok(dataService.Persons);            
        }

        [HttpGet("{id}")]        
        public ActionResult<IEnumerable<Person>> Get(int id)
        {
            dataService.Persons = reposetory.loadData();            

            bool personExists = dataService.Persons.Any(person => person.Id == id);
            if (personExists)
            {
                Person person = dataService.Persons.Find(person => person.Id == id);                
                return Ok(person);
            }
            return NoContent();
        }

        [HttpDelete, BasicAuthorization]     
        public IActionResult Remove(int id)
        {
            dataService.Persons = reposetory.loadData();
            dataService.Cars = carReposetory.loadData();

            Person person = dataService.Persons.Find(p => p.Id == id);
            if (person == null) 
                return NotFound();

            if (person.CarId != null)
            {
                Car car = dataService.Cars.Find(a => a.Id == person.CarId);
                if (car != null)
                {
                    car.PersonId = null;
                }
            }

            dataService.Persons.Remove(person);

            reposetory.saveData(dataService.Persons);
            carReposetory.saveData(dataService.Cars);

            return NoContent();
        }

        [HttpPost, BasicAuthorization]      
        public IActionResult Post(Person person)
        {
            int id;
            if (dataService.Persons.Count > 0)
                id = dataService.Persons.Max(p => p.Id) + 1;
            else
                id = 0;

            person.Id = id;

            if (person.CarId != null)
            {
                Car car = dataService.Cars.Find(p => p.Id == person.CarId);

                if (car == null) 
                    person.CarId = null;
                else 
                    car.PersonId = person.Id;
            }            
            dataService.Persons.Add(person);

            reposetory.saveData(dataService.Persons);
            carReposetory.saveData(dataService.Cars);

            return CreatedAtAction("Get", new { id = person.Id }, person);
        }

        [HttpPut, BasicAuthorization]
        public IActionResult Put(Person person)
        {
            if (person == null)
            {
                return NoContent();
            }

            int index = dataService.Persons.FindIndex(p => p.Id == person.Id);
            
            if (index == -1)
            {
                return NoContent();
            }

            Remove(person.Id);
            Post(person);           
            
            return Ok();
        }


    }
}