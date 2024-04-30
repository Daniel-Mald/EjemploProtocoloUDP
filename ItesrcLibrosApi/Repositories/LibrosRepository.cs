using ItesrcLibrosApi.Models.Entities;

namespace ItesrcLibrosApi.Repositories
{
    public class LibrosRepository
    {
        ItesrcneLibrosContext _context { get; }
        public LibrosRepository(ItesrcneLibrosContext context)
        {
            _context = context;
        }
        public IEnumerable<Libros> GetAll()
        {
            return _context.Libros.OrderBy(x => x.Titulo);
        }
        public Libros? GetById(int id)
        {
            return _context.Libros.Find(id);
        }
        public void Insert(Libros l)
        {
            _context.Libros.Add(l);
            _context.SaveChanges();
        }
        public void Update(Libros l)
        {
            _context.Libros.Update(l);
            _context.SaveChanges();
        }
        public void Delete(Libros l)
        {
            _context.Libros.Remove(l);
            _context.SaveChanges();
        }

    }
}
