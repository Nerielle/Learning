using System;
using System.Web.Mvc;
using AutoMapper;
using Shop.Models;
using Shop.Services;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class CartsController : Controller
    {
        private readonly CartService _cartService = new CartService();

        public CartsController()
        {
            Mapper.CreateMap<Cart, CartViewModel>();
            Mapper.CreateMap<CartItem, CartItemViewModel>();
            Mapper.CreateMap<Book, BookViewModel>();
            Mapper.CreateMap<Author, AuthorViewModel>();
            Mapper.CreateMap<Category, CategoryViewModel>();
        }

        // GET: Carts
        public ActionResult Index()
        {
            var cart = _cartService.GetBySessionId(HttpContext.Session.SessionID);

            return View(
              AutoMapper.Mapper.Map<Cart, CartViewModel>(cart)
            );
        }

        [ChildActionOnly]
        public PartialViewResult Summary()
        {
            var cart = _cartService.GetBySessionId(HttpContext.Session.SessionID);

            return PartialView(
                Mapper.Map<Cart, CartViewModel>(cart)
                );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cartService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
