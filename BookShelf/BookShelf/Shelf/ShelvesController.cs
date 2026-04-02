using AutoMapper;
using BookShelf.Api.Shelf.Contract;
using BookShelf.Model.Shelf;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Api.Shelf;


[ApiController]
[Route("api/v1/shelves")]
public class ShelvesController : ControllerBase
{
    private readonly IShelfOrchestrator _orchestrator;
    private readonly IMapper _mapper;

    public ShelvesController(
        IShelfOrchestrator orchestrator,
        IMapper mapper)
    {
        _orchestrator = orchestrator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShelfAsync(CreateShelf shelf)
    {
        var shelfDto = _mapper.Map<ShelfDto>(shelf);

        var result = await _orchestrator.CreateShelfAsync(shelfDto);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetShelvesAsync()
    {
        var entities = await _orchestrator.GetShelvesAsync();

        var response = _mapper.Map<List<GetShelf>>(entities);

        return Ok(response);
    }
}