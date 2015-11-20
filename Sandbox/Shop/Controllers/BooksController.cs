using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Shop.Models;
using Shop.Services;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService = new BookService();

        public BooksController()
        {
            Mapper.CreateMap<Book, BookViewModel>();
            Mapper.CreateMap<Author, AuthorViewModel>();
            Mapper.CreateMap<Category, CategoryViewModel>();
        }

        // GET: Books
        public ActionResult Index(int categoryId)
        {
            var books = _bookService.GetByCategoryId(categoryId);
            ViewBag.SelectedCategoryId = categoryId;

            return View(
              AutoMapper.Mapper.Map<List<Book>, List<BookViewModel>>(books)
            );
        }

        [ChildActionOnly]
        public PartialViewResult Featured()
        {
            var books = _bookService.GetFeatured();

            return PartialView(
                Mapper.Map<List<Book>, List<BookViewModel>>(books)
                );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bookService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
