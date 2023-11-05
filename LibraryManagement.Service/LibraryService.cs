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

   public void BorrowBook(Book book, User user)
   {
      if (!_library.IsBookAvailable(book))
         throw new BookNotAvailableException("Book not available");
      if (user.IsLimitExceededToBorrowTheBook())
         throw new UserNotEligibleToBorrowException("Only 2 books can be borrowed at a time");
      user.BorrowBook(book);
      _library.RemoveBookFromInventory(book);
   }
}