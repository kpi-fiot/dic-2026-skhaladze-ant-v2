using AutoMapper;
using BookShelf.Api.Shelf.Contract;
using BookShelf.Model.Shelf;

namespace BookShelf.Api.Shelf;

public class ShelfMap : Profile
{
    public ShelfMap()
    {
        CreateMap<CreateShelf, ShelfDto>();
        CreateMap<ShelfDto, GetShelf>();
    }
}
