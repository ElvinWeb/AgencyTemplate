using Agency.Business.CustomExceptions;
using Agency.Business.Services;
using Agency.Core.Models;
using Agency.Core.Repositories;
using Agency.MVC.Pagination;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;

namespace Agency.MVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _categoryService.GetCategoryTable();

            PaginatedList<Category> paginatedCategories = PaginatedList<Category>.Create(query, page, 4);

            return View(paginatedCategories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _categoryService.CreateAsync(category);
            }
            catch (InvalidAlreadyCreated ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null) return NotFound();

            Category category = await _categoryService.GetByIdAsync(id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _categoryService.UpdateAsync(category);
            }
            catch (InvalidAlreadyCreated ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            await _categoryService.Delete(id);

            return Ok();
        }

    }
}

