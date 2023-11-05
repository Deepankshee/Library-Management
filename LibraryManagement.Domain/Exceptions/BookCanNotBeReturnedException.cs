namespace LibraryManagement.Domain.Exceptions;

public class BookCanNotBeReturnedException : Exception
{
    public BookCanNotBeReturnedException(string message) : base(message)
    {
        
    }
}