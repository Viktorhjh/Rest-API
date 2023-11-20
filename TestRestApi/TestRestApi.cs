using Lab_2;
using Lab_2.Controllers;
using Lab_2.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace TestRestApi
{
    public class TestRestApi
    {
        [Fact]
        public void Test()
        {
            var mock = new Mock<DataService>();            
            var controller = new PersonController(mock.Object);
            
            //Act
            var res = controller.Get();    
            
            // Assert                        
            Assert.IsType<OkObjectResult>(res.Result);
        }

        [Fact]
        public void TestId()
        {            
            var mock = new Mock<DataService>();                        
            var controller = new PersonController(mock.Object);   
            
            int id = 0;

            //Act            
            var res = controller.Get(id);     
            
            // Assert                        
            Assert.IsType<OkObjectResult>(res.Result);
        }

        public void TestPerson()
        {
            var mock = new Mock<DataService>();
                
        }
    }
}