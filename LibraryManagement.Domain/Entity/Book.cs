namespace LibraryManagement.Domain.Entity;

public class Book
{
    public Book(string isbn, string title, string author)
    {
        ISBN = isbn;
        Title = title;
        Author = author;
    }
    public string ISBN { get; }
    public string Title { get; }
    public string Author { get; }
}