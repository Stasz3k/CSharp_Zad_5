namespace REST_API_PJATK.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using REST_API_PJATK.Models;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("[controller]")]
    public class VisitsController : ControllerBase
    {
        private static List<Visit> visits = new List<Visit>
    {
        new Visit { Id = 1, AnimalId = 1, DateOfVisit = DateTime.Now, Description = "Szczepienie roczne", Price = 150.0m },
        new Visit { Id = 2, AnimalId = 2, DateOfVisit = DateTime.Now.AddDays(-1), Description = "Kontrola zdrowia", Price = 100.0m }
    };

        [HttpGet]
        public ActionResult<IEnumerable<Visit>> GetVisits()
        {
            if (!visits.Any())
            {
                return NoContent(); // Zwróć 204 No Content, jeśli lista jest pusta
            }
            return Ok(visits); // Zwróć 200 OK z listą wizyt
        }

        [HttpGet("animal/{animalId}")]
        public ActionResult<IEnumerable<Visit>> GetVisitsForAnimal(int animalId)
        {
            var animalVisits = visits.Where(v => v.AnimalId == animalId).ToList();
            if (!animalVisits.Any())
            {
                return NotFound(); // Zwróć 404 Not Found, jeśli nie ma wizyt dla danego zwierzęcia
            }
            return Ok(animalVisits); // Zwróć 200 OK z listą wizyt dla zwierzęcia
        }

        [HttpPost]
        public ActionResult<Visit> CreateVisit([FromBody] Visit visit)
        {
            visits.Add(visit);
            return CreatedAtAction(nameof(GetVisitsForAnimal), new { animalId = visit.AnimalId }, visit); // Zwróć 201 Created z lokalizacją nowego zasobu
        }
    }

}
