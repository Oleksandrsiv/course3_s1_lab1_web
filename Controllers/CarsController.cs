using Microsoft.AspNetCore.Mvc;
using course3_s1_lab1_web.Data;
using course3_s1_lab1_web.Models;

namespace course3_s1_lab1_web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Car>> GetAll()
    {
        return Ok(DataStore.Cars); 
    }


    [HttpGet("{id}")]
    public ActionResult<Car> GetById(int id)
    {
        var car = DataStore.Cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        return Ok(car);
    }


    [HttpPost]
    public ActionResult<Car> Create(Car newCar)
    {
        var factoryExists = DataStore.Factories.Any(f => f.Id == newCar.FactoryId);
        if (!factoryExists)
        {
            return BadRequest("Фабрики з таким FactoryId не існує."); 
        }

        newCar.Id = DataStore.Cars.Any() ? DataStore.Cars.Max(c => c.Id) + 1 : 1;
        DataStore.Cars.Add(newCar);
        
        return CreatedAtAction(nameof(GetById), new { id = newCar.Id }, newCar);
    }


    [HttpPut("{id}")]
    public IActionResult Update(int id, Car updatedCar)
    {
        var car = DataStore.Cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        
        var factoryExists = DataStore.Factories.Any(f => f.Id == updatedCar.FactoryId);
        if (!factoryExists)
        {
            return BadRequest("Фабрики з таким FactoryId не існує.");
        }

        car.ModelName = updatedCar.ModelName;
        car.FactoryId = updatedCar.FactoryId;
        
        return NoContent();
    }
    

    [HttpDelete]
    public IActionResult DeleteAll()
    {
        DataStore.Cars.Clear();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
        var car = DataStore.Cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        
        DataStore.Cars.Remove(car);
        return NoContent();
    }
}