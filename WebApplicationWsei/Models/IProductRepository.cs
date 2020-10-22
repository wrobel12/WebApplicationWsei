using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationWsei.Models
{
    public interface IProductRepository
    {
       public IQueryable<Product> Products { get; }
    }
}
