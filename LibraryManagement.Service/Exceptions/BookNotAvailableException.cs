namespace LibraryManagement.Service.Exceptions;

public class BookNotAvailableException : Exception
{
    public BookNotAvailableException(string message) :base(message)
    {
        
    }
}