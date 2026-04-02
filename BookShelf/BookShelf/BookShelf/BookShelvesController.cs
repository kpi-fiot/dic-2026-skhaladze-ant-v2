using System.ComponentModel.DataAnnotations;
using BookShelf.Model.BookShelf;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Api.BookShelf;


[ApiController]
[Route("api/v1/shelves")]
public class BookShelvesController : ControllerBase
{
    private readonly IBooksShelfOrchestrator _booksShelfOrchestrator;

    public BookShelvesController(IBooksShelfOrchestrator booksShelfOrchestrator)
    {
        _booksShelfOrchestrator = booksShelfOrchestrator;
    }

    [HttpPost("{shelfId}/books/{bookId}")]
    public async Task<IActionResult> CreateBookShelfAsync(
        [Required]Guid shelfId, 
        [Required]int bookId)
    {
        var model = await _booksShelfOrchestrator.CreateBookShelfAsync(shelfId, bookId);

        return Ok(model);
    }

    [HttpGet("{shelfId}/books")]
    public async Task<IActionResult> GetBookShelvesAsync([Required] Guid shelfId)
    {
        var bookIds = await _booksShelfOrchestrator.GetBookShelvesAsync(shelfId);
        
        return Ok(bookIds);
    }

    [HttpGet("{shelfId}/books/{bookId}")]
    public async Task<IActionResult> GetBookFromShelfAsync(
        [Required] Guid shelfId,
        [Required] int bookId)
    {
        var model = await _booksShelfOrchestrator.GetBookFromShelfAsync(shelfId, bookId);

        return Ok(model);
    }
}
