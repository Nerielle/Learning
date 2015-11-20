using System;
using System.Collections.Generic;
using System.Linq;
using Shop.DAL;
using Shop.Models;

namespace Shop.Services
{
    public class BookService
    {
        private readonly ShopDbContext _db = new ShopDbContext();

        public List<Book> GetFeatured()
        {
            return _db.Books.
                Include("Author").
                Where(b => b.Featured).
                ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
