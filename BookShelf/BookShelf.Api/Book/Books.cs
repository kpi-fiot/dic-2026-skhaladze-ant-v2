using AutoMapper;
using BookkShelf.Model.Book;
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
    public async Task<IActionResult> PostAsync(PostBook postBook)
    {
        var book = _mapper.Map<BookDto>(postBook);

        var createdBook = await _bookOrchestrator.CreateAsync(book);

        var response = _mapper.Map<GetBook>(createdBook);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var books = await _bookOrchestrator.GetAllAsync();

        var response = _mapper.Map<List<GetBook>>(books);

        return Ok(response);
    }
}