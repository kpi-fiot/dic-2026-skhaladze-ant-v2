using BookkShelf.Model.Book;
using BookShelf.Model.Book;

namespace BookShelf.Orchestartor.Book;

public class BookOrchestrator : IBookOrchestrator
{
    private readonly IBookRepository _bookRepository;

    public BookOrchestrator(
        IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto> CreateAsync(BookDto book)
    {
        return await _bookRepository.CreateAsync(book);
    }

    public async Task<List<BookDto>> GetAllAsync()
    {
        return await _bookRepository.GetAllAsync();
    }
}