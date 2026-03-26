using BookShelf.Dal.Shelf;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.Dal;

public class CosmosDbContext : DbContext
{
    public CosmosDbContext (DbContextOptions<CosmosDbContext> options) : base(options)
    {
    }

    public DbSet<ShelfDao> Shelves { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShelfDao>().HasKey(s => s.Id);
        modelBuilder.Entity<ShelfDao>().HasPartitionKey(s => s.Id);
        modelBuilder.Entity<ShelfDao>().Property(s => s.Id).ToJsonProperty("id");
        modelBuilder.HasDefaultContainer("shelves");
        
        
    }
}
