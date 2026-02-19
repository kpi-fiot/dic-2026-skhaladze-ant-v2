namespace BookkShelf.Model.Book;

public class BookDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDttmUtc { get; set; }
    public int PageCount { get; set; }
}