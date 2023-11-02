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
}