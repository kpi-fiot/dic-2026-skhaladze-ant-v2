namespace BookShelf.Model.Shelf;

public interface IShelfOrchestrator
{
    Task<List<ShelfDto>> GetShelvesAsync();
    Task<ShelfDto> CreateShelfAsync(ShelfDto shelf);
}