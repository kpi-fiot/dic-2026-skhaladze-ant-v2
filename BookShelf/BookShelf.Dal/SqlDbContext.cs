using BookShelf.Dal.Book;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.Dal;

public class SqlDbContext : DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
    {
    }

    public DbSet<BookDao> Books { get; set; }
}