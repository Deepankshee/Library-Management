using LibraryManagement.Domain.Entity;
using LibraryManagement.Domain.Repository;

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
      _library.RemoveBookFromInventory(book);
      user.AddBook(book);

   }
   
   public void ReturnBook(Book book, User user)
   {
       user.RemoveBook(book);
      _library.AddBookToInventory(book);
   }
}
