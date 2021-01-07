using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationWsei.Models;

namespace WebApplicationWsei.Controllers
{
    [Route("api/[controller]")]
    public class ApiProductController : Controller
    {
        private readonly IProductRepository repository;

        public ApiProductController(IProductRepository repository)
        {
            this.repository = repository;
        }
        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="category">category name</param>
        /// <returns>Products list</returns>
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Product>> List(string category)
        {
            return Ok(repository.Products.Where(p => p.category == category));
        }
        [HttpGet("GetById")]
        public ActionResult<Product> GetById(int id)
        {
            var product = repository.Products.SingleOrDefault(p => p.productID == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            repository.SaveProduct(product);
            return Ok(product);
            //            return CreatedAtAction(nameof(GetById), new { id = product.ProductID }, product);
        }

        [HttpDelete]
        public ActionResult DeleteProduct(int productId)
        {
            repository.DeleteProduct(productId);
            return NoContent();
        }
        [HttpPut]
        public ActionResult UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!repository.Products.Any(p => p.productID == product.productID))
                return NotFound();

            repository.SaveProduct(product);

            return NoContent();
        }
    }
}