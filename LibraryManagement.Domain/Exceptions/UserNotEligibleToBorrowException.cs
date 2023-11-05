namespace LibraryManagement.Domain.Exceptions;

public class UserNotEligibleToBorrowException : Exception
{
    public UserNotEligibleToBorrowException(string message) : base(message)
    {
        
    }
}