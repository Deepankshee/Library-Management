using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Domain.Entity;

public class User
{
    private readonly List<Book> _borrowedBooks = new();
    private const int NoOfBooksUserCanBorrowAtAtime = 2;
    
    public void AddBook(Book book) {
        
        if (IsLimitExceededToBorrowTheBook())
            throw new UserNotEligibleToBorrowException("Only 2 books can be borrowed at a time");
        
        if(IsBookBorrowed(book))
            throw new UserNotEligibleToBorrowException("Only 1 copy of a book can be borrowed at a time");
        
        _borrowedBooks.Add(book);
    }

    public void RemoveBook(Book book)
    {
        if (!IsBookBorrowed(book))
            throw new BookCanNotBeReturnedException("Book can not be returned if it is not borrowed earlier");
        _borrowedBooks.Remove(book);
    }
    
    private bool IsLimitExceededToBorrowTheBook()
    {
        return _borrowedBooks.Count == NoOfBooksUserCanBorrowAtAtime;
    }
    
    private bool IsBookBorrowed(Book book)
    {
        return _borrowedBooks.Any(item => item == book);
    }
    
}