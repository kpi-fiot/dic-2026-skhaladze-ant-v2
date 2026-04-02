using BookShelf.Model.Book;
using BookShelf.Model.BookShelf;
using BookShelf.Model.Exception;
using BookShelf.Model.Shelf;
using BookShelf.Platform.BlobStorage;

namespace BookShelf.Orchestrator.BookShelf;

public class BookShelfOrchestrator : IBooksShelfOrchestrator
{
    private readonly IShelfOrchestrator _shelfOrchestrator;
    private readonly IBookOrchestrator _bookOrchestrator;
    private readonly IBlobStorage _blobStorage;

    public BookShelfOrchestrator(
        IShelfOrchestrator shelfOrchestrator,
        IBookOrchestrator bookOrchestrator,
        IBlobStorage blobStorage)
    {
        _shelfOrchestrator = shelfOrchestrator;
        _bookOrchestrator = bookOrchestrator;
        _blobStorage = blobStorage;
    }

    public async Task<BookShelfDto> CreateBookShelfAsync(Guid shelfId, int bookId)
    {
        var book = await _bookOrchestrator.GetBookByIdAsync(bookId);
        var shelf = (await _shelfOrchestrator.GetShelvesAsync()).FirstOrDefault(c => c.Id == shelfId);

        var fileName = $"{shelfId}_{bookId}";

        var exists = await _blobStorage.ExistsAsync(fileName);

        if(!exists)
        {
            await _blobStorage.UploadBlobAsync(fileName);
        }

        return new BookShelfDto
        {
            ShelfId = shelfId,
            BookId = bookId
        };
    }

    public async Task<BookDto> GetBookFromShelfAsync(Guid shelfId, int bookId)
    {
        var book = await _bookOrchestrator.GetBookByIdAsync(bookId);
        var shelf = (await _shelfOrchestrator.GetShelvesAsync()).FirstOrDefault(c => c.Id == shelfId);

        var fileName = $"{shelfId}_{bookId}";

        var exists = await _blobStorage.ExistsAsync(fileName);

        if (!exists)
        {
            throw new EntityNotFound($"Book with id {bookId} is not on shelf with id {shelfId}");
        }

        return book;
    }

    public async Task<IEnumerable<int>> GetBookShelvesAsync(Guid shelfId)
    {
        var shelf = (await _shelfOrchestrator.GetShelvesAsync()).FirstOrDefault(c => c.Id == shelfId);

        return await _blobStorage.GetAllFilesNameAsync(shelfId);
    }
}
