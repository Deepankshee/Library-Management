using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Domain.Entity;

public class Library
{
    private readonly Dictionary<Book, int> _bookInventory;

    public Library(Dictionary<Book, int> bookInventory)
    {
        _bookInventory = bookInventory;
    }
    
    public Dictionary<Book, int> GetAvailableBooks()
    {
        return _bookInventory.Where(item => item.Value > 0)
            .ToDictionary(item => item.Key, item => item.Value);
    }
    
    public int GetBookQuantity(Book book)
    {
        return _bookInventory.Where(item => item.Key == book).Select(item=>item.Value).FirstOrDefault();
    }

    public void RemoveBookFromInventory(Book book)
    {
        if (!IsBookAvailable(book))
            throw new BookNotAvailableException("Book not available");
        _bookInventory[book] -= 1;
    }

    public void AddBookToInventory(Book book)
    {
        _bookInventory[book] += 1;
    }
    
    private bool IsBookAvailable(Book book)
    {
        return _bookInventory.Any(item => item.Key == book && item.Value > 0);
    }
}