using Microsoft.AspNetCore.Mvc;
using SklStore.Models;
using System.Diagnostics;
using SklStore.Models.ViewModels;

namespace SklStore.Controllers
{
    public class HomeController : Controller
    {

        private IStoreRepository repository;
        public int PageSize = 4;
        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(string category, string searchQuery, int productPage = 1)
{
    var query = repository.Products
        .Where(p => category == null || p.Category == category);

    if (!string.IsNullOrEmpty(searchQuery))
    {
        query = query.Where(p => p.Name.Contains(searchQuery));
    }

    var products = query
        .OrderBy(p => p.ProductID)
        .Skip((productPage - 1) * PageSize)
        .Take(PageSize);

    var viewModel = new ProductListViewModel
    {
        Products = products,
        PagingInfo = new PagingInfo
        {
            CurrentPage = productPage,
            ItemsPerPage = PageSize,
            TotalItems = category == null ?
                repository.Products.Count() :
                repository.Products.Count(e => e.Category == category)
        },
        CurrentCategory = category,
        SearchQuery = searchQuery
    };

    return View(viewModel);
}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}