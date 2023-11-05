namespace LibraryManagement.Domain.Exceptions;

public class BookNotAvailableException : Exception
{
    public BookNotAvailableException(string message) :base(message)
    {
        
    }
}