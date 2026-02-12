using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShelf.Dal.Book;

[Table("books", Schema = "dbo")]
public class BookDao
{
    [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("release_date_utc")]
    public DateTime PublishedDate { get; set; }
    [Column("page_count")]
    public int PageCount { get; set; }
}