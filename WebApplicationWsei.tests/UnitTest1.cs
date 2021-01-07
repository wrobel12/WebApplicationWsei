using System;
using Xunit;
using WebApplicationWsei.Models;
using Moq;
using System.Linq;
using WebApplicationWsei.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationWsei.tests
{
    public class UnitTest1
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            // Przygotowanie — tworzenie imitacji repozytorium.
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {productID = 1, name = "P1"},
                new Product {productID = 2, name = "P2"},
                new Product {productID = 3, name = "P3"},
            }.AsQueryable<Product>());

            // Przygotowanie — utworzenie kontrolera.
            AdminController controller = new AdminController(mock.Object);
            // Dzia³anie.
            Product[] result =
                GetViewModel<IEnumerable<Product>>(controller.Index())?.ToArray();

            // Asercje.
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].name);
            Assert.Equal("P2", result[1].name);
            Assert.Equal("P3", result[2].name);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            // Przygotowanie.
            // Utworzenie imitacji repozytorium.
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {productID = 1, name = "P1", category = "Cat1"},
                new Product {productID = 2, name = "P2", category = "Cat2"},
                new Product {productID = 3, name = "P3", category = "Cat1"},
                new Product {productID = 4, name = "P4", category = "Cat2"},
                new Product {productID = 5, name = "P5", category = "Cat3"}
            }).AsQueryable<Product>());

            // Przygotowanie — utworzenie kontrolera i ustawienie 3-elementowej strony.
            ProductController controller = new ProductController(mock.Object);

            // Dzia³anie.
            Product[] result = GetViewModel<IEnumerable<Product>>(controller.List("Cat2")).ToArray();

            // Asercje.
            Assert.Equal(2, result.Length);
            Assert.True(result[0].name == "P2" && result[0].category == "Cat2");
            Assert.True(result[1].name == "P4" && result[1].category == "Cat2");
        }

        [Theory]
        [InlineData(1, "P1")]
        [InlineData(4, "P4")]
        public void Can_Get_Products(int id, string expectedName)
        {
            // Przygotowanie.
            // Utworzenie imitacji repozytorium.
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {productID = 1, name = "P1", category = "Cat1"},
                new Product {productID = 2, name = "P2", category = "Cat2"},
                new Product {productID = 3, name = "P3", category = "Cat1"},
                new Product {productID = 4, name = "P4", category = "Cat2"},
                new Product {productID = 5, name = "P5", category = "Cat3"}
            }).AsQueryable<Product>());

            // Przygotowanie — utworzenie kontrolera i ustawienie 3-elementowej strony.
            ProductController controller = new ProductController(mock.Object);

            // Dzia³anie.
            Product result = GetViewModel<Product>(controller.GetById(id));

            // Asercje.
            Assert.Equal(result.name, expectedName);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
