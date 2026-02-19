using AutoMapper;
using BookkShelf.Model.Book;
using BookShelf.Dal.Book;

namespace BookShelf.Dal;

public class DaoProfile : Profile
{
    public DaoProfile()
    {
        CreateMap<BookDto, BookDao>().ReverseMap();
    }
}