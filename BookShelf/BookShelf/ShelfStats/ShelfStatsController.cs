using BookShelf.Model.MessageBroker;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Api.ShelfStats;

[ApiController]
[Route("api/v1/shelves-stats")]
public class ShelfStatsController : ControllerBase
{
    private readonly ISubscriber _subscriber;

    public ShelfStatsController(ISubscriber subscriber)
    {
        _subscriber = subscriber;
    }

    [HttpGet]
    public async Task<IActionResult> GetShelfStatsAsync()
    {
        return Ok(_subscriber.Data);
    }
}