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
}