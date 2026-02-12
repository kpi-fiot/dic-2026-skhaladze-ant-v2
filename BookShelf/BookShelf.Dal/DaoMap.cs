using AutoMapper;
using BookShelf.Model.Book;

namespace BookShelf.Dal;

public class DaoMap : Profile
{
    public DaoMap()
    {
        CreateMap<BookDto, Book.BookDao>().ReverseMap();
    }
}