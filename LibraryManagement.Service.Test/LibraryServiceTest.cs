using FluentAssertions;
using LibraryManagement.Domain.Entity;
using LibraryManagement.Domain.Repository;
using Moq;

namespace LibraryManagement.Service.Test;

public class LibraryServiceTest
{
    [Fact]
    public void ShouldReturnBooksIfPresent()
    {
        var libraryRepository = new Mock<ILibraryRepository>();
        var book = new Book("123", "2 States", "Chetan Bhagat");
        var expectedBooks = new List<Book> { book };
        var bookInventory = new Dictionary<Book, int>()
        {
            {book,2}
        };
        libraryRepository.Setup(x => x.GetBookInventory()).Returns(bookInventory);
        LibraryService libraryService = new LibraryService(libraryRepository.Object);
        
        var result = libraryService.GetBooks();

        result.Should().BeEquivalentTo(expectedBooks);
    }
    
    [Fact]
    public void ShouldReturnEmptyListIfNoBooksArePresent()
    {
        var libraryRepository = new Mock<ILibraryRepository>();
        var bookInventory = new Dictionary<Book, int>();
        libraryRepository.Setup(x => x.GetBookInventory()).Returns(bookInventory);
        LibraryService libraryService = new LibraryService(libraryRepository.Object);
        
        var result = libraryService.GetBooks();

        result.Should().BeEmpty();
    }
}