namespace LibraryManagement.Domain.Entity;

public class Library
{
    private Dictionary<Book, int> bookDB;

    public Library(Dictionary<Book, int> bookDb)
    {
        bookDB = bookDb;
    }
    
    public Dictionary<Book, int> GetAvailableBooks()
    {
        return bookDB.Where(item => item.Value > 0)
            .ToDictionary(item => item.Key, item => item.Value);
    }
    
    public int GetBookQuantity(Book book)
    {
        return bookDB.Where(item => item.Key == book).Select(item=>item.Value).FirstOrDefault();
    }

    public void RemoveBookFromInventory(Book book)
    {
        bookDB[book] -= 1;
    }

    public bool IsBookAvailable(Book book)
    {
        return bookDB.Any(item => item.Key == book && item.Value > 0);
    }
}