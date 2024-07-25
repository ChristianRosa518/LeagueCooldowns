using CooldownsServer.Models;
using CooldownsServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace CooldownsServer.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ChampionController : ControllerBase {

    private readonly ChampionServices _championService;
    public ChampionController(ChampionServices championService) => _championService = championService;

    [HttpGet]
    public async Task<List<Champion>> Get() =>
        await _championService.GetAsync();

    [HttpGet("{name}")]
    public async Task<ActionResult<Champion>> Get(string name)
    {
        var champion = await _championService.ReturnChampionByName(name);

        if (champion is null)
        {
            return NotFound();
        }
        return champion;
    }
    [HttpPost]
    public async Task<IActionResult> Post(Champion newChampion)
    {   
        var checkChamp = await _championService.ReturnChampionByName(newChampion.ChampionName);
       
        if (checkChamp is null)
        {
            await _championService.CreateAsync(newChampion);
            return CreatedAtAction(nameof(Get), new {id = newChampion.Id}, newChampion);
        }
        return BadRequest("Champion exists");
    }

    [HttpPut("{name}")]
    public async Task<IActionResult> Update(string name, Champion updatedChampion)
    {
        var champion = await _championService.ReturnChampionByName(name);

        if(champion is null)
        {
            return NotFound();
        }

        updatedChampion.Id = champion.Id;

        await _championService.UpdateAsync(champion.Id, updatedChampion);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var champion = await _championService.GetAsync(id);

        if (champion is null)
        {
            return NotFound();
        }

        await _championService.RemoveAsync(id);

        return NoContent();
    }
}
