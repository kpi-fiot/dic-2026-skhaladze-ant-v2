using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookShelf.Dal.Shelf;

public class ShelfDao
{
    public Guid Id { get; set; }
    [Range(3, 100)]
    public string Name { get; set; }
    public int Capacity { get; set; }
    public DateTime CreatedAt { get; set; }
}
