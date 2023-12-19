using Agency.Business.Services;
using Agency.Core.Models;
using Agency.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Agency.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        public HomeController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }
        public async Task<IActionResult> Index()
        {
            List<Portfolio> portfolios = await _portfolioService.GetAllAsync();

            return View(portfolios);
        }
        public async Task<IActionResult> Modal()
        {
            

            return View();
        }


    }
}