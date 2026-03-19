using AutoMapper;
using BookShelf.Model.Book;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.Dal.Book;

public class BookRepository : IBookRepository
{
    private readonly IMapper _mapper;
    private readonly SqlDbContext _context;

    public BookRepository(
        IMapper mapper,
        SqlDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<BookDto> CreateBookAsync(BookDto book)
    {
        var entity = _mapper.Map<BookDao>(book);

        var entry = _context.Books.Add(entity);

        await _context.SaveChangesAsync();

        return _mapper.Map<BookDto>(entry.Entity);
    }

    public async Task<BookDto> GetBookByIdAsync(int bookId)
    {
        var entity = await _context.Books
            .AsNoTracking()
            .SingleOrDefaultAsync(c=>c.Id == bookId);

        return _mapper.Map<BookDto>(entity);
    }
}