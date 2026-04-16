using BookShelf.Model.MessageBroker;
using BookShelf.Model.Shelf;

namespace BookShelf.Orchestrator.Shelf;

public class ShelfOrchestrator : IShelfOrchestrator
{
    private readonly IPublisher _statsPublisher;
    private readonly IShelfRepository _repository;

    public ShelfOrchestrator(
        IPublisher statsPublisher,
        IShelfRepository repository)
    {
        _statsPublisher = statsPublisher;
        _repository = repository;
    }
    public async Task<List<ShelfDto>> GetShelvesAsync()
    {
        return await _repository.GetShelvesAsync();
    }

    public async Task<ShelfDto> CreateShelfAsync(ShelfDto shelf)
    {
        var createdEntity = await _repository.CreateShelfAsync(shelf);

        await _statsPublisher.PublishAsync(createdEntity.Id);

        return createdEntity;
    }
}