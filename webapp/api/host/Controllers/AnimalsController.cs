using System.ComponentModel.DataAnnotations;
using domain;
using host.Models;
using Microsoft.AspNetCore.Mvc;

namespace host.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalsController(IAnimalService animalService) => _animalService = animalService;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnimalViewModel>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<AnimalViewModel>> Get()
    {
        var animals = await _animalService.GetAll();
        return AnimalViewModel.Map(animals);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AnimalViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(AnimalCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToString());
        }
        var result = await _animalService.Add(request.Name!);
        return new ObjectResult(new AnimalViewModel(result)) { StatusCode = StatusCodes.Status201Created };
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([Range(1, int.MaxValue)] int id)
    {
        try
        {
            await _animalService.Delete(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
