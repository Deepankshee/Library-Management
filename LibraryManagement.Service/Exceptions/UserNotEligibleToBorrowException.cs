namespace LibraryManagement.Service.Exceptions;

public class UserNotEligibleToBorrowException : Exception
{
    public UserNotEligibleToBorrowException(string message) : base(message)
    {
        
    }
}