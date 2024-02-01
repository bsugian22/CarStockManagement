using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private static List<Car> carStock = new List<Car>();

    [HttpPost]
    public IActionResult AddCar([FromBody] Car car)
    {
        carStock.Add(car);
        return Ok();
    }

    [HttpDelete("{make}/{model}")]
    public IActionResult RemoveCar(string make, string model)
    {
        var carToRemove = carStock.Find(c => c.Make == make && c.Model == model);
        if (carToRemove != null)
        {
            carStock.Remove(carToRemove);
            return Ok();
        }
        return NotFound();
    }

    [HttpGet]
    public IActionResult ListCars()
    {
        return Ok(carStock);
    }

    [HttpPatch("{make}/{model}")]
    public IActionResult UpdateStock(string make, string model, [FromBody] int newStock)
    {
        var carToUpdate = carStock.Find(c => c.Make == make && c.Model == model);
        if (carToUpdate != null)
        {
            carToUpdate.Year = newStock;
            return Ok();
        }
        return NotFound();
    }

    [HttpGet("search")]
    public IActionResult SearchCar([FromQuery] string make, [FromQuery] string model)
    {
        var result = carStock.FindAll(c => c.Make == make && c.Model == model);
        return Ok(result);
    }
}
