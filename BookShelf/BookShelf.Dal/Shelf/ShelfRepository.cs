using AutoMapper;
using BookShelf.Model.Shelf;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.Dal.Shelf;

public class ShelfRepository : IShelfRepository
{
    private readonly CosmosDbContext _context;
    private readonly IMapper _mapper;

    public ShelfRepository(
        CosmosDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ShelfDto>> GetShelvesAsync()
    {
        var entity = await _context.Shelves.ToListAsync();

        return _mapper.Map<List<ShelfDto>>(entity);
    }

    public async Task<ShelfDto> CreateShelfAsync(ShelfDto shelf)
    {
        try
        {
            var entity = _mapper.Map<ShelfDao>(shelf);
            //entity.Id = Guid.NewGuid();

            var result = await _context.Shelves.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ShelfDto>(result.Entity);
        }
        catch (Exception e)
        {
            var t = 1;
            throw;
        }
    }
}