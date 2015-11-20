using System;
using System.Collections.Generic;
using System.Linq;
using Shop.DAL;
using Shop.Models;

namespace Shop.Services
{
    public class CartService : IDisposable
    {
        private readonly ShopDbContext _db = new ShopDbContext();

        public void Dispose()
        {
            _db.Dispose();
        }

        public Cart GetBySessionId(string sessionId)
        {
            var cart = _db.Carts.Include("CartItems").
                SingleOrDefault(c => c.SessionId == sessionId);

            cart = CreateCartIfItDoesntExist(sessionId, cart);

            return cart;
        }

        private Cart CreateCartIfItDoesntExist(string sessionId, Cart cart)
        {
            if (null == cart)
            {
                cart = new Cart
                {
                    SessionId = sessionId,
                    CartItems = new List<CartItem>()
                };
                _db.Carts.Add(cart);
                _db.SaveChanges();
            }

            return cart;
        }
    }
}
