using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Lab_2.Models
{
    public class PersonReposetory
    {
        private static string filePath = "personData.json";     
        private List<Person> persons = new List<Person>();        

        public void saveData(List<Person> data)
        {
            persons = data;
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine($"JSON data saved to {filePath}");
        }

        public List<Person> loadData()
        {
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                persons = JsonSerializer.Deserialize<List<Person>>(data);
                Console.WriteLine($"JSON data load from {filePath}");
            }
            else
            {
                File.Create(filePath);
                File.WriteAllText(filePath, "[]");
                Console.WriteLine($"JSON file created");
            }

            return persons;            
        }
    }
}
