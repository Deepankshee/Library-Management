using FluentAssertions;
using LibraryManagement.Domain.Entity;
using LibraryManagement.Domain.Exceptions;
using Xunit;

namespace LibraryManagement.Domain.Test;

public class UserTests
{   
    [Fact]
    public void ShouldNotBeAbleToAddBookInBorrowedListWhenLimitExceeds()
    {
        User user = new User();
        Book firstBook = new Book("123", "5 AM Club", "Robin Sharma");
        Book secondBook = new Book("1234", "Monk who sold his ferrari", "Robin Sharma");
        Book thirdBook = new Book("123", "The Alchemist", "Robin Sharma");
        
        user.AddBook(firstBook);
        user.AddBook(secondBook);
        Action action = () =>user.AddBook(thirdBook);

        action.Should().Throw<UserNotEligibleToBorrowException>().WithMessage("Only 2 books can be borrowed at a time");
    }
    
    [Fact]
    public void ShouldNotBeAbleToAddSameBookInBorrowedListTwice()
    {
        User user = new User();
        Book book = new Book("123", "5 AM Club", "Robin Sharma");

        user.AddBook(book);
        Action action = () =>user.AddBook(book);

        action.Should().Throw<UserNotEligibleToBorrowException>().WithMessage("Only 1 copy of a book can be borrowed at a time");
    }

    [Fact]
    public void ShouldNotBeAbleToRemoveFromBorrowedListWhenBookIsNotPresentInTheList()
    {
        User user = new User();
        Book book = new Book("123", "5 AM Club", "Robin Sharma");
        
        Action action = () =>user.RemoveBook(book);

        action.Should().Throw<BookCanNotBeReturnedException>().WithMessage("Book can not be returned if it is not borrowed earlier");
    }
}