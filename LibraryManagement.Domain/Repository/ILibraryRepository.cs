using LibraryManagement.Domain.Entity;

namespace LibraryManagement.Domain.Repository;

public interface ILibraryRepository
{
    public Dictionary<Book, int> GetBookInventory();
}