namespace BookShelf.Model.Book;

public interface IBookOrchestrator
{
    Task<BookDto> CreateBookAsync(BookDto book);
    Task<BookDto> GetBookByIdAsync(int bookId);
}