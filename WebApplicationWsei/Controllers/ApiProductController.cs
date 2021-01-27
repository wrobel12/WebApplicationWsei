using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationWsei.Models;

namespace WebApplicationWsei.Controllers
{
    [Route("api/Products")]
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
        /// <returns>Products list</returns>
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Product>> List()
        {
            IEnumerable<Product> list = repository.Products;
            return Ok(list);
        }

        /// <summary>
        /// Get products by category
        /// </summary>
        /// <param name="category">category name</param>
        /// <returns>Products list</returns>
        [HttpGet("GetByCategory/{category}")]
        public ActionResult<IEnumerable<Product>> List(string category)
        {
            IEnumerable<Product> list = repository.Products.Where(p => p.category == category);
            if (list.Count() == 0)
                return NotFound();
            return Ok(list);
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>Product based on given ID</returns>
        /// <response code="201">Returns product</response>
        /// <response code="400">If there is no product with given ID</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("GetById/{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = repository.Products.SingleOrDefault(p => p.productID == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// Adding new product
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST /AddProduct
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "description": "descOfItem1",
        ///        "category": "categoryOfItem1",
        ///        "price": 0.0
        ///     }
        /// </remarks>
        /// <param name="product">product</param>
        /// <returns>Added product</returns>
        [HttpPost("Add/{product}")]
        public ActionResult<Product> AddProduct(Product product)
        {
            repository.SaveProduct(product);
            return Ok(product);
        }


        /// <summary>
        /// Deletes an existing product in the database
        /// </summary>
        /// <param name="productId">product id</param>
        /// <returns>Deleted product</returns>
        [HttpDelete("Delete/{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            var product = repository.Products.SingleOrDefault(p => p.productID == productId);
            if (product == null)
                return NotFound();
            repository.DeleteProduct(productId);
            return Ok(product);
            // return NoContent();
        }

        /// <summary>
        /// Updates an existing product in the database
        /// </summary>
        /// <param name="product">product</param>
        /// <returns>Updated product</returns>
        [HttpPut("Update/{product}")]
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