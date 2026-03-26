namespace BookShelf.Model.Shelf;

public class ShelfDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public DateTime CreatedAt { get; set; }
}