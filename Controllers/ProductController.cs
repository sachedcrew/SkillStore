using Microsoft.AspNetCore.Mvc;
using SklStore.Models;
using System.Collections.Generic;

namespace SklStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IStoreRepository _repository;

        public ProductController(IStoreRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            List<Product> products = _repository.GetProducts();
            return View(products);
        }

        public IActionResult Details(long id)
        {
            Product product = _repository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
