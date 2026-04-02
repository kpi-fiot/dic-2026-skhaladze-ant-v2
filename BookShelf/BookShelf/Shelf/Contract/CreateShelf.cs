using System.ComponentModel.DataAnnotations;

namespace BookShelf.Api.Shelf.Contract;

public class CreateShelf
{
    [Length(3, 100)]
    public string Name { get; set; }
    [Range(1, 100)]
    public int Capacity { get; set; }
    [Range(typeof(DateTime), "01/01/1890", "01/01/2025")]
    public DateTime CreatedAt { get; set; }
}