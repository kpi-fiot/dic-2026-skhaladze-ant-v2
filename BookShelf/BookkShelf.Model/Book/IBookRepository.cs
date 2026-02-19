using BookkShelf.Model.Book;

namespace BookShelf.Model.Book;

public interface IBookRepository
{
    Task<BookDto> CreateAsync(BookDto book);
    Task<List<BookDto>> GetAllAsync();
}
