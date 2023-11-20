using Lab_2.Models;
using System.Text.Json;

namespace Lab_2.Reposetory
{
    public class CarReposetory
    {
        private static string filePath = "carData.json";
        private List<Car> cars = new List<Car>();        

        public void saveData(List<Car> data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine($"JSON data saved to {filePath}");            
        }

        public List<Car> loadData()
        {
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                cars = JsonSerializer.Deserialize<List<Car>>(data);
                Console.WriteLine($"JSON data load from {filePath}");
            }
            else
            {
                File.Create(filePath);
                File.WriteAllText(filePath, "[]");
                Console.WriteLine($"JSON file created");
            }
            return cars;            
        }

    }
}
