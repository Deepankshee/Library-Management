using LibraryManagement.Domain.Entity;
using LibraryManagement.Domain.Repository;
using LibraryManagement.Service.Exceptions;

namespace LibraryManagement.Service;

public class LibraryService
{
   private readonly Library _library;

   public LibraryService(ILibraryRepository libraryRepository)
   {
      _library = new Library(libraryRepository.GetBookInventory());
   }
   public List<Book> GetBooks() {
      return _library.GetAvailableBooks().Select(item=>item.Key).ToList();
   }

   public int GetBookQuantity(Book book)
   {
      return _library.GetBookQuantity(book);
   }

   public void BorrowBook(Book book, User user)
   {
      if (!_library.IsBookAvailable(book))
         throw new BookNotAvailableException("Book not available");
      
      if (user.IsLimitExceededToBorrowTheBook())
         throw new UserNotEligibleToBorrowException("Only 2 books can be borrowed at a time");
      
      if(user.IsBookAlreadyBorrowed(book))
         throw new UserNotEligibleToBorrowException("Only 1 copy of a book can be borrowed at a time");
      
      user.BorrowBook(book);
      _library.RemoveBookFromInventory(book);
   }
   
   public void ReturnBook(Book book, User user)
   {
      user.ReturnBook(book);
      _library.AddBookToInventory(book);
   }
}