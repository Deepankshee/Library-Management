using FluentAssertions;
using LibraryManagement.Domain.Entity;
using LibraryManagement.Domain.Exceptions;
using Xunit;

namespace LibraryManagement.Domain.Test;

public class LibraryTests
{
    [Fact]
    public void ShouldNotBeAbleToRemoveFromInventoryWhenBookIsNotAvailable()
    {
        var firstBook = new Book("123", "2 States", "Chetan Bhagat");
        var secondBook =new Book("1234", "Robin Sharma", "The 5 AM Club");
        Dictionary<Book, int> bookInventory = new Dictionary<Book, int>
        {
            {firstBook ,2}
        };
        Library library = new Library(bookInventory);
        
        Action action = () =>library.RemoveBookFromInventory(secondBook);;

        action.Should().Throw<BookNotAvailableException>().WithMessage("Book not available");
    }
    
    [Fact]
    public void ShouldBeAbleToRemoveFromInventoryWhenBookIsAvailable()
    {
        var firstBook = new Book("123", "2 States", "Chetan Bhagat");
        Dictionary<Book, int> bookInventory = new Dictionary<Book, int>
        {
            {firstBook ,2}
        };
        Library library = new Library(bookInventory);
        
        library.RemoveBookFromInventory(firstBook);;

        library.GetBookQuantity(firstBook).Should().Be(1);
    }
    
    [Fact]
    public void ShouldBeAbleToAddBookToInventory()
    {
        var firstBook = new Book("123", "2 States", "Chetan Bhagat");
        Dictionary<Book, int> bookInventory = new Dictionary<Book, int>
        {
            {firstBook ,1}
        };
        Library library = new Library(bookInventory);
        
        library.AddBookToInventory(firstBook);;

        library.GetBookQuantity(firstBook).Should().Be(2);
    }
}