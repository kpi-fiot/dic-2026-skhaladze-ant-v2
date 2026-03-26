namespace BookShelf.Model.Shelf;

public interface IShelfRepository
{
    Task<List<ShelfDto>> GetShelvesAsync();
    Task<ShelfDto> CreateShelfAsync(ShelfDto shelf);
}