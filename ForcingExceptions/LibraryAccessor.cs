using Microsoft.EntityFrameworkCore;

namespace ForcingExceptions
{
    public class LibraryAccessor : IDisposable
    {
        LibraryContext _context;

        public static void Prepopulate()
        {
            using var accessor = new LibraryAccessor();

            var a1 = new Author { Name = "Jane Austen" };
            var a2 = new Author { Name = "Ian Fleming" };

            var b1 = new Book { Title = "Emma", Year = 1815, Author = a1 };
            var b2 = new Book { Title = "Persuasion", Year = 1818, Author = a1 };
            var b3 = new Book { Title = "Dr No", Year = 1958, Author = a2 };
            var b4 = new Book { Title = "Goldfinger", Year = 1959, Author = a2 };

            accessor._context.Add(b1);
            accessor._context.Add(b2);
            accessor._context.Add(b3);
            accessor._context.Add(b4);

            accessor._context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public LibraryAccessor()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: "QuickDB")
                 .Options;

            _context = new LibraryContext(options);
        }

        public Task<IEnumerable<Book>> ByDate(int lowYear, int highYear)
        {
            if (highYear < lowYear)
                throw new ArgumentException($"{highYear} is earlier than {lowYear}");

            return AsyncBit();

            async Task<IEnumerable<Book>> AsyncBit()
            {
                var query = from b in _context.Books.Include(b => b.Author) where b.Year >= lowYear && b.Year <= highYear select b;

                return await query.ToArrayAsync();
            }
        }
    }
}
