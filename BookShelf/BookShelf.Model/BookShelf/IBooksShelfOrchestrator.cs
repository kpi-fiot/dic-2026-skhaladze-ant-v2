using BookShelf.Model.Book;

namespace BookShelf.Model.BookShelf;

public interface IBooksShelfOrchestrator
{
    Task<BookShelfDto> CreateBookShelfAsync(Guid shelfId, int bookId);
    Task<BookDto> GetBookFromShelfAsync(Guid shelfId, int bookId);
    Task<IEnumerable<int>> GetBookShelvesAsync(Guid shelfId);
}
