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
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(AnimalDeleteRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToString());
        }

        try
        {
            await _animalService.Delete(request.Id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
