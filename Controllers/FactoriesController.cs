using Microsoft.AspNetCore.Mvc;
using course3_s1_lab1_web.Data;
using course3_s1_lab1_web.Models;

namespace course3_s1_lab1_web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FactoriesController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Factory>> GetAll()
    {
        return Ok(DataStore.Factories); 
    }
    

    [HttpGet("{id}")]
    public ActionResult<Factory> GetById(int id)
    {
        var factory = DataStore.Factories.FirstOrDefault(f => f.Id == id);
        if (factory == null)
        {
            return NotFound();
        }
        return Ok(factory);
    }


    [HttpPost]
    public ActionResult<Factory> Create(Factory newFactory)
    {
        newFactory.Id = DataStore.Factories.Any() ? DataStore.Factories.Max(f => f.Id) + 1 : 1;
        DataStore.Factories.Add(newFactory);
        
        return CreatedAtAction(nameof(GetById), new { id = newFactory.Id }, newFactory);
    }


    [HttpPut("{id}")]
    public IActionResult Update(int id, Factory updatedFactory)
    {
        var factory = DataStore.Factories.FirstOrDefault(f => f.Id == id);
        if (factory == null)
        {
            return NotFound();
        }
        
        factory.Name = updatedFactory.Name;
        return NoContent(); 
    }


    [HttpDelete]
    public IActionResult DeleteAll()
    {
        DataStore.Factories.Clear();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
        var factory = DataStore.Factories.FirstOrDefault(f => f.Id == id);
        if (factory == null)
        {
            return NotFound();
        }
        
        DataStore.Factories.Remove(factory);
        return NoContent();
    }
    
    
    [HttpGet("{factoryId}/cars")]
    public ActionResult<IEnumerable<Car>> GetCarsByFactory(int factoryId)
    {
        var factoryExists = DataStore.Factories.Any(f => f.Id == factoryId);
        if (!factoryExists)
        {
            return NotFound();
        }
        
        var cars = DataStore.Cars.Where(c => c.FactoryId == factoryId).ToList();
        return Ok(cars);
    }
}