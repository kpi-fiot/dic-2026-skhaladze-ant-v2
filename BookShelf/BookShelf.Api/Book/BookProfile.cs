using AutoMapper;
using BookkShelf.Model.Book;
using BookShelf.Api.Book.Contract;

namespace BookShelf.Api.Book;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<PostBook, BookDto>().ReverseMap();
        CreateMap<BookDto, GetBook>().ReverseMap();
    }
}