namespace BookShelf.Model.Book;

public interface IBookRepository
{
    Task<BookDto> CreateBookAsync(BookDto book);
    Task<BookDto> GetBookByIdAsync(int bookId);
}