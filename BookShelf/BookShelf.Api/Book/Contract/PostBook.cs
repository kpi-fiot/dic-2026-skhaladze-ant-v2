using System.ComponentModel.DataAnnotations;

namespace BookShelf.Api.Book.Contract;

public class PostBook
{
    [Length(3, 128)]
    public string Name { get; set; }
    [Range(typeof(DateTime), "1/1/1900", "1/1/2026")]
    public DateTime CreatedDttmUtc { get; set; }
    [Range(20, 1000)]
    public int PageCount { get; set; }
}