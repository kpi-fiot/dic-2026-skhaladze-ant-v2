using BookShelf.Api.Book.Contract;
using Newtonsoft.Json;
using FluentAssertions;
using BookShelf.Dal.Book;

namespace BookShelf.IntegrationTests.Book;

public class CreateBookAsyncTests : BaseTest
{
    private HttpClient _httpClient;
    public CreateBookAsyncTests()
    {
        _httpClient = InitTestServer().GetClient();
    }

    [Fact]
    public async Task GetByIdAsync_IfBookExists_ReturnsBook()
    {
        // Arrange
        const int bookId = 102020;
        var bookDao = new BookDao
        {
            Id = bookId,
            Name = $"Name {bookId}",
            PageCount = 1000,
            PublishedDate = new DateTime(2022, 1, 1)
        };
        SqlDbContext.Books.Add(bookDao);
        await SqlDbContext.SaveChangesAsync();

        // Act
        var result = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"api/v1/books/{bookId}"));

        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var createdBook = JsonConvert.DeserializeObject<GetBook>(await result.Content.ReadAsStringAsync());
        createdBook.Should().NotBeNull();
        createdBook.Should().BeEquivalentTo(bookDao);
    }
}