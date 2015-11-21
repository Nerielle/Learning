using System;
using System.Collections.Generic;
using System.Linq;
using Shop.DAL;
using Shop.Models;

namespace Shop.Services
{
    public class CategoryService : IDisposable
    {
        private readonly ShopDbContext _db = new ShopDbContext();

        public void Dispose()
        {
            _db.Dispose();
        }

        public List<Category> Get()
        {
            return _db.Categories.OrderBy(c => c.Name).ToList();
        }
    }
}