using BookShelf.Model.Shelf;

namespace BookShelf.Orchestrator.Shelf;

public class ShelfOrchestrator : IShelfOrchestrator
{
    private readonly IShelfRepository _repository;

    public ShelfOrchestrator(IShelfRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<ShelfDto>> GetShelvesAsync()
    {
        return await _repository.GetShelvesAsync();
    }

    public Task<ShelfDto> CreateShelfAsync(ShelfDto shelf)
    {
        return _repository.CreateShelfAsync(shelf);
    }
}