namespace LibraryManagement.Domain.Entity;

public class User
{
    private List<Book> _borrowedBooks = new();
    public const int NoOfBooksUserCanBorrowAtAtime = 2;
    
    public void BorrowBook(Book book) {
        _borrowedBooks.Add(book);
    }
    
    public bool IsLimitExceededToBorrowTheBook()
    {
        return _borrowedBooks.Count == NoOfBooksUserCanBorrowAtAtime;
    }
    
    public bool IsBookAlreadyBorrowed(Book book)
    {
        return _borrowedBooks.Any(item => item == book);
    }
}