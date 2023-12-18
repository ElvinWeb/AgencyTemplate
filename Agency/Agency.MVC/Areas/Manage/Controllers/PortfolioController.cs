using Agency.Business.CustomExceptions.PortfolioExceptions;
using Agency.Business.Services;
using Agency.Core.Models;
using Agency.MVC.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agency.MVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        private readonly ICategoryService _categoryService;
        public PortfolioController(IPortfolioService portfolioService, ICategoryService categoryService)
        {
            _portfolioService = portfolioService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _portfolioService.GetPortfolioTable().Include(category => category.Category).Where(portfolio => portfolio.IsDeleted == false);
            PaginatedList<Portfolio> paginatedPortfolio = PaginatedList<Portfolio>.Create(query, page, 3);

            return View(paginatedPortfolio);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            ViewBag.Categories = await _categoryService.GetAllAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Portfolio portfolio)
        {


            ViewBag.Categories = await _categoryService.GetAllAsync();


            if (!ModelState.IsValid) return View();

            try
            {
                await _portfolioService.CreateAsync(portfolio);
            }
            catch (InvalidContentTypeAndSize ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();

            }
            catch (InvalidImage ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            ViewBag.Categories = await _categoryService.GetAllAsync();

            if (id == null) return NotFound();

            Portfolio portfolio = await _portfolioService.GetByIdAsync(id);

            if (portfolio == null) return NotFound();

            return View(portfolio);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Portfolio team)
        {

            ViewBag.Categories = await _categoryService.GetAllAsync();


            if (!ModelState.IsValid) return View();

            try
            {
                await _portfolioService.UpdateAsync(team);
            }
            catch (InvalidContentTypeAndSize ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();

            }
            catch (InvalidImage ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            ViewBag.Categories = await _categoryService.GetAllAsync();


            if (id == null) return NotFound();

            try
            {
                await _portfolioService.SoftDelete(id);
            }
            catch (NullReferenceException)
            {

            }

            return Ok();
        }
    }
}
