using LibraryManagement.Domain.Entity;
using LibraryManagement.Domain.Repository;

namespace LibraryManagement.Repository;

public class LibraryRepository : ILibraryRepository
{
    public Dictionary<Book, int> GetBookInventory()
    {
        Dictionary<Book, int> bookInventory = new Dictionary<Book, int>();
        bookInventory.Add(GenerateBook("123","Chetan Bhagat","2 states"),2);
        bookInventory.Add(GenerateBook("456","Robin Sharma","Monk who sold his ferrari"),3);
        bookInventory.Add(GenerateBook("789","Robin Sharma","The 5 AM club"),2);

        return bookInventory;
    }

    private Book GenerateBook(string ISBN, string author, string title)
    {
        return new Book(ISBN, title, author);
    }
}