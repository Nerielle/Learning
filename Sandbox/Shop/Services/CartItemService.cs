using System;
using System.Data.Entity;
using System.Linq;
using Shop.DAL;
using Shop.Models;

namespace Shop.Services
{
    public class CartItemService : IDisposable
    {
        private readonly ShopDbContext _db = new ShopDbContext();

        public void Dispose()
        {
            _db.Dispose();
        }

        public CartItem GetByCartIdAndBookId(int cartId, int bookId)
        {
            return _db.CartItems.SingleOrDefault(ci => ci.CartId == cartId &&
                                                       ci.BookId == bookId);
        }

        public CartItem AddToCart(CartItem cartItem)
        {
            var existingCartItem = GetByCartIdAndBookId(cartItem.CartId, cartItem.BookId);

            if (null == existingCartItem)
            {
                _db.Entry(cartItem).State = EntityState.Added;
                existingCartItem = cartItem;
            }
            else
            {
                existingCartItem.Quantity += cartItem.Quantity;
            }

            _db.SaveChanges();

            return existingCartItem;
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            _db.Entry(cartItem).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            _db.Entry(cartItem).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
