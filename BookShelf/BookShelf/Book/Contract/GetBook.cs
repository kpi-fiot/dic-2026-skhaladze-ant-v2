namespace BookShelf.Api.Book.Contract;

public class GetBook
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime PublishedDate { get; set; }
    public int PageCount { get; set; }
}