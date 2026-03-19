using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BookShelf.Api.Book.Contract;
using BookShelf.Model.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Api.Book;

[ApiController]
[Route("api/v1/books")]
public class BooksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBookOrchestrator _bookOrchestrator;

    public BooksController(
        IMapper mapper,
        IBookOrchestrator bookOrchestrator)
    {
        _mapper = mapper;
        _bookOrchestrator = bookOrchestrator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookAsync(CreateBook request)
    {
        var book = _mapper.Map<BookDto>(request);

        var createdBook = await _bookOrchestrator.CreateBookAsync(book);

        var response = _mapper.Map<GetBook>(createdBook);

        return Ok(response);
    }

    [HttpGet("{bookId:int}")]
    public async Task<IActionResult> GetByIdAsync([Range(1, int.MaxValue)] int bookId)
    {
        var book = await _bookOrchestrator.GetBookByIdAsync(bookId);

        var response = _mapper.Map<GetBook>(book);

        return Ok(response);
    }

}
