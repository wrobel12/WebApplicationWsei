using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplicationWsei.Models
{
    public class Product
    {
        public int productID { get; set; }
        
        [Display(Name = "Nazwa")]
        public string name { get; set; }

        [Display(Name = "Opis")]
        public string description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Proszę podać dodatnią cenę")]
        [Display(Name = "Cena label")]
        public decimal price { get; set; }

        [Display(Name = "Kategoria")]
        public string category { get; set; }


    }
}
