using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories
{
    public class FakeBookRepository : IBookRepository
    {
        private static readonly ICollection<Book> _books = new List<Book>
        {
            new Book(1, "Władca Pierścieni: Drużyna Pierścienia", 29.99m, "Powieść fantasy J.R.R. Tolkiena opisująca podróż małej grupy przyjaciół w celu zniszczenia potężnego pierścienia.", "image1.jpg"),
            new Book(2, "Harry Potter i Kamień Filozoficzny", 24.99m, "Pierwsza część serii o młodym czarodzieju Harrym Potterze, napisana przez J.K. Rowling.", "image2.jpg"),
            new Book(3, "Wojna i pokój", 34.99m, "Epicka powieść Lwa Tołstoja, opowiadająca o życiu rosyjskiej arystokracji w okresie wojen napoleońskich.", "image3.jpg"),
            new Book(4, "1984", 27.99m, "Dystopijna powieść George'a Orwella opisująca totalitarny reżim i kontrolę nad ludźmi.", "image4.jpg"),
            new Book(5, "Hobbit", 21.99m, "Powieść fantasy J.R.R. Tolkiena opisująca podróż małej grupy przyjaciół w celu odbicia domu kransoludów spod władzy smoka czarnoksiężnika który jest..", "image5.jpg")
        };


        public IEnumerable<Book> GetAll() 
        {
            return _books.ToList();
        }

        public Book Get(int id) 
        {
            return _books.FirstOrDefault(x => x.Id == id);
        }

    }
}
