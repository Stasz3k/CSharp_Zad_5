namespace REST_API_PJATK.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using REST_API_PJATK.Models;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("[controller]")]
    public class AnimalsController : ControllerBase
    {
        private static List<Animal> animals = new List<Animal>
    {
        new Animal { Id = 1, Name = "Max", Category = "Pies", Weight = 30.0, FurColor = "Czarny" },
        new Animal { Id = 2, Name = "Mia", Category = "Kot", Weight = 5.0, FurColor = "Biały" }
    };

        [HttpGet]
        public ActionResult<IEnumerable<Animal>> GetAnimals()
        {
            if (animals.Count == 0)
            {
                return NoContent(); // Zwróć 204 No Content, jeśli lista jest pusta
            }
            return Ok(animals); // Zwróć 200 OK z listą zwierząt
        }

        [HttpGet("{id}")]
        public ActionResult<Animal> GetAnimal(int id)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null)
            {
                return NotFound(); // Zwróć 404 Not Found, jeśli zwierzę nie istnieje
            }
            return Ok(animal); // Zwróć 200 OK z danymi zwierzęcia
        }

        [HttpPost]
        public ActionResult<Animal> CreateAnimal([FromBody] Animal animal)
        {
            animals.Add(animal);
            return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal); // Zwróć 201 Created z lokalizacją nowego zasobu
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAnimal(int id, [FromBody] Animal updatedAnimal)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null)
            {
                return NotFound(); // Zwróć 404 Not Found, jeśli zwierzę nie istnieje
            }
            animal.Name = updatedAnimal.Name;
            animal.Category = updatedAnimal.Category;
            animal.Weight = updatedAnimal.Weight;
            animal.FurColor = updatedAnimal.FurColor;
            return NoContent(); // Zwróć 204 No Content po pomyślnej aktualizacji
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAnimal(int id)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null)
            {
                return NotFound(); // Zwróć 404 Not Found, jeśli zwierzę nie istnieje
            }
            animals.Remove(animal);
            return NoContent(); // Zwróć 204 No Content po pomyślnym usunięciu
        }
    }

}
