using FluentAssertions;
using LibraryManagement.Domain.Entity;
using LibraryManagement.Domain.Repository;
using LibraryManagement.Service.Exceptions;
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
    public void ShouldNotBeAbleToBorrowTheBookWhenBookIsNotPresentInLibrary()
    {
        var libraryRepository = new Mock<ILibraryRepository>();
        var book = new Book("123", "2 States", "Chetan Bhagat");
        var bookToBorrow =new Book("1234", "Robin Sharma", "The 5 AM Club");
        var bookInventory = new Dictionary<Book, int>()
        {
            {book,2}
        };
        libraryRepository.Setup(x => x.GetBookInventory()).Returns(bookInventory);
        LibraryService libraryService = new LibraryService(libraryRepository.Object);
        
        Action action = () =>libraryService.BorrowBook(bookToBorrow, new User());

        action.Should().Throw<BookNotAvailableException>();
    }
    
    [Fact]
    public void ShouldNotBeAbleToBorrowTheBookWhenBookQuantityIsZeroInLibrary()
    {
        var libraryRepository = new Mock<ILibraryRepository>();
        var book = new Book("123", "2 States", "Chetan Bhagat");
        var bookInventory = new Dictionary<Book, int>()
        {
            {book,0}
        };
        libraryRepository.Setup(x => x.GetBookInventory()).Returns(bookInventory);
        LibraryService libraryService = new LibraryService(libraryRepository.Object);
        
        Action action = () =>libraryService.BorrowBook(book, new User());

        action.Should().Throw<BookNotAvailableException>();
    }
    
    [Fact]
    public void ShouldNotBeAbleToBorrowTheBookWhenUserBorrowedBooksLimitExceeded()
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
        Action action = () =>libraryService.BorrowBook(firstBook, user);

        action.Should().Throw<UserNotEligibleToBorrowException>().WithMessage("Only 2 books can be borrowed at a time");
    }
    
    [Fact]
    public void ShouldBeAbleToBorrowTheBookAndUpdateTheBookInventory()
    {
        var libraryRepository = new Mock<ILibraryRepository>();
        var firstBook = new Book("123", "2 States", "Chetan Bhagat");
        var user = new User();
        var bookInventory = new Dictionary<Book, int>()
        {
            {firstBook,1},
        };
        libraryRepository.Setup(x => x.GetBookInventory()).Returns(bookInventory);
        LibraryService libraryService = new LibraryService(libraryRepository.Object);
        
        libraryService.BorrowBook(firstBook,user);
        
        libraryService.GetBooks().Should().BeEmpty();
    }
}