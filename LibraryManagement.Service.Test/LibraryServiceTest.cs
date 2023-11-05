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

    [Fact]
    public void ShouldBeAbleToBorrowTheBookAndUpdateTheBookInventory()
    {
        var libraryRepository = new Mock<ILibraryRepository>();
        var book = new Book("123", "2 States", "Chetan Bhagat");
        var user = new User();
        var bookInventory = new Dictionary<Book, int>()
        {
            {book,2},
        };
        libraryRepository.Setup(x => x.GetBookInventory()).Returns(bookInventory);
        LibraryService libraryService = new LibraryService(libraryRepository.Object);
        
        libraryService.BorrowBook(book,user);
        
        libraryService.GetBookQuantity(book).Should().Be(1);
    }
    
    
    [Fact]
    public void ShouldBeAbleToReturnTheBookAndUpdateTheBookInventory()
    {
        var libraryRepository = new Mock<ILibraryRepository>();
        var firstBook = new Book("123", "2 States", "Chetan Bhagat");
        var secondBook =new Book("1234", "Robin Sharma", "The 5 AM Club");
        var user = new User();
        var bookInventory = new Dictionary<Book, int>()
        {
            {firstBook,2},
            {secondBook,2}
        };
        libraryRepository.Setup(x => x.GetBookInventory()).Returns(bookInventory);
        LibraryService libraryService = new LibraryService(libraryRepository.Object);
        
        libraryService.BorrowBook(firstBook,user);
        libraryService.BorrowBook(secondBook,user);
        libraryService.ReturnBook(firstBook,user);

        libraryService.GetBookQuantity(firstBook).Should().Be(2);
    }

}