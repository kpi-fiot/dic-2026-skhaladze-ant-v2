using AutoMapper;
using BookkShelf.Model.Book;
using BookShelf.Model.Book;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.Dal.Book;

public class BookRepository : IBookRepository
{
    private readonly SqlDbContext _sqlDbContext;
    private readonly IMapper _mapper;

    public BookRepository(
        SqlDbContext sqlDbContext,
        IMapper mapper)
    {
        _sqlDbContext = sqlDbContext;
        _mapper = mapper;
    }

    public async Task<BookDto> CreateAsync(BookDto book)
    {
        var entity = _mapper.Map<BookDao>(book);

        var createdBook = _sqlDbContext.Books.Add(entity);

        await _sqlDbContext.SaveChangesAsync();

        return _mapper.Map<BookDto>(createdBook.Entity);

    }

    public async Task<List<BookDto>> GetAllAsync()
    {
        var entities = await _sqlDbContext.Books.AsNoTracking().ToListAsync();

        return _mapper.Map<List<BookDto>>(entities);
    }
}