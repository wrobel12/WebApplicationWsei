using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationWsei.Models;

namespace WebApplicationWsei.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index() => View(repository.Products);

        public ViewResult Edit(int productID) =>
            View(repository.Products
                .FirstOrDefault(p => p.productID == productID));


        [HttpPost]

        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"Zapisano {product.name}.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        public IActionResult Delete(int productID)
        {
            Product deletedProduct = repository.DeleteProduct(productID);
            if (deletedProduct != null)
            {
                TempData["message"] = $"Usunięto {deletedProduct.name}.";
            }
            return RedirectToAction("Index");




        }
    }
}