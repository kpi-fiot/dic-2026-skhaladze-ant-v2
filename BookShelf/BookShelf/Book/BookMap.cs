using AutoMapper;
using BookShelf.Api.Book.Contract;
using BookShelf.Model.Book;

namespace BookShelf.Api.Book;

public class BookMap : Profile
{
    public BookMap()
    {
        CreateMap<CreateBook, BookDto>();
        CreateMap<BookDto, GetBook>();
    }
}
