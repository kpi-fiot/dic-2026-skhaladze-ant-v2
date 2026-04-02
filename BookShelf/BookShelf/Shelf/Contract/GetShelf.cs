namespace BookShelf.Api.Shelf.Contract;

public class GetShelf
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public DateTime CreatedAt { get; set; }
}