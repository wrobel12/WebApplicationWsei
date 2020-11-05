using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationWsei.Models;

namespace WebApplicationWsei.Controllers
{
    public class ProductController : Controller
    {
        IQueryable<Product> repository;


        public ProductController(IProductRepository repository)
        {
            this.repository = repository.Products;
        }

        public IProductRepository Irepository { get; }

        public IActionResult Index()
        {
            return View();
        }



        public ViewResult ListAll() => View(this.repository);

        public ViewResult List(string category)
        {
            if (category == null)
            {
                return View(this.repository);
            } else {
                return View(this.repository.Where(p => p.category == category));
            }
        }

    }
}