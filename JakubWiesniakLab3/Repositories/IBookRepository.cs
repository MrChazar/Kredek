using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        
        Book? Get(int id);
    }
}
