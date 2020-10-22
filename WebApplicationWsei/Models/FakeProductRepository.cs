using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace WebApplicationWsei.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product> {
            new Product { name = "Piłka nożna", price = 25 },
            new Product { name = "Buty do biegania", price = 95 }
       }.AsQueryable();


    }

    }
