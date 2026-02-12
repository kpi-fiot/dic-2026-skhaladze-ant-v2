using BookShelf.Model.Book;
using BookShelf.Model.Exception;

namespace BookShelf.Orchestrator.Book;

public class BookOrchestrator : IBookOrchestrator
{
    private readonly IBookRepository _repository;

    public BookOrchestrator(
        IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<BookDto> CreateBookAsync(BookDto book)
    {
        return await _repository.CreateBookAsync(book);
    }

    public async Task<BookDto> GetBookByIdAsync(int bookId)
    {
        var book = await _repository.GetBookByIdAsync(bookId);

        if (book == null)
        {
            throw new EntityNotFound($"Book with id {bookId} not found");
        }

        return book;
    }
}