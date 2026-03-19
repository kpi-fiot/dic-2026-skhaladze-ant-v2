using BookShelf.Model.Book;
using BookShelf.Model.Exception;
using BookShelf.Orchestrator.Book;
using FluentAssertions;
using Moq;

namespace BookShelf.OrchestartorTests.Book;

public class BookOrchestratorTests
{
    [Fact]
    public async Task GetBookByIdAsync_IfBookDoesntExist_ThrowException()
    {
        // Arrange
        const int id = 1;
        var repository = new Mock<IBookRepository>();
        repository
            .Setup(x => x.GetBookByIdAsync(id))!
            .ReturnsAsync((BookDto)null);
        var orchestrator = new BookOrchestrator(repository.Object);

        // Act
        await Assert.ThrowsAsync<EntityNotFound>(async () => await orchestrator.GetBookByIdAsync(id));
    }

    [Fact]
    public async Task GetBookByIdAsync_IfBookExists_ReturnsBook()
    {
        // Arrange
        const int id = 1;
        var book = new BookDto
        {
            Id = id,
            Name = $"Name {id}",
            PageCount = id + 100,
            PublishedDate = new DateTime(2022, 1, 1)
        };
        var expectedBook = new BookDto
        {
            Id = id,
            Name = $"Name {id}",
            PageCount = id + 100,
            PublishedDate = new DateTime(2022, 1, 1)
        };

        var repository = new Mock<IBookRepository>();
        repository
            .Setup(x => x.GetBookByIdAsync(id))
            .ReturnsAsync(book);
        var orchestrator = new BookOrchestrator(repository.Object);

        // Act
        var result = await orchestrator.GetBookByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedBook);
    }
}