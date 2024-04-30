using LibrosMaui.Models.Entities;
using SQLite;
//using SQLite.Net;
//using SQLite.Net.Interop;
//using SQLite.Net.Platform.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrosMaui.Repositories
{
    public class LibrosRepository
    {
        SQLiteConnection _context;

        public LibrosRepository()
        {
            string ruta = FileSystem.AppDataDirectory + "/libros.db3";
            _context = new SQLiteConnection( ruta);
            _context.CreateTable<Libro>();
        }
        public IEnumerable<Libro> GetAll()
        {
            return _context.Table<Libro>().
                OrderBy(x => x.Titulo);
                
        }
        public Libro? Get(int id)
        {
            return _context.Find<Libro>(id);
        }
        public void InsertOrReplace(Libro l)
        {
            _context.InsertOrReplace(l);
        }
        public void Insert(Libro _libro)
        {
            _context.Insert(_libro);
        }
        public void Update(Libro _libro)
        {
            _context.Update(_libro);
        }
        public void Delete(Libro _libro)
        {
            _context.Delete(_libro);
        }

    }
}
