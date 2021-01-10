using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationWsei.Models;

namespace WebApplicationWsei.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductRepository _productRepository;

        public NavigationMenuViewComponent(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            dynamic mymodel = new ExpandoObject();
            mymodel.modelCategory = _productRepository.Products.Select(x => x.category).Distinct().OrderBy(x => x);
            mymodel.products = _productRepository;

            //return View(_productRepository.Products);
            return View(mymodel);

        }
    }
}
