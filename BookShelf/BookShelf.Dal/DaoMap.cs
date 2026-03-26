using AutoMapper;
using BookShelf.Model.Book;
using BookShelf.Model.Shelf;

namespace BookShelf.Dal;

public class DaoMap : Profile
{
    public DaoMap()
    {
        CreateMap<BookDto, Book.BookDao>().ReverseMap();
        CreateMap<Shelf.ShelfDao, ShelfDto>().ReverseMap();
    }
}
