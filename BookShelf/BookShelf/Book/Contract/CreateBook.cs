using System.ComponentModel.DataAnnotations;

namespace BookShelf.Api.Book.Contract;

public class CreateBook
{
    [Length(3,128)]
    public string Name { get; set; }
    [Range(typeof(DateTime), "01/01/1890", "01/01/2025")]
    public DateTime PublishedDate { get; set; }
    [Range(5, 1000)]
    public int PageCount { get; set; }
}