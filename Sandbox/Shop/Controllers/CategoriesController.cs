using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Shop.Models;
using Shop.Services;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryService _categoryService = new CategoryService();

        [ChildActionOnly]
        public PartialViewResult Menu(int selectedCategoryId)
        {
            var categories = _categoryService.Get();

            Mapper.CreateMap<Category, CategoryViewModel>();

            ViewBag.SelectedCategoryId = selectedCategoryId;

            return PartialView(Mapper.Map<List<Category>, List<CategoryViewModel>>(categories));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _categoryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}