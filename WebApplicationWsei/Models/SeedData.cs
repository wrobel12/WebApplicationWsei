using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationWsei.Models;

namespace WebApplication9.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
                .GetRequiredService<AppDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        name = "Kajak",
                        description = "Łódka przeznaczona dla jednej osoby",
                        category = "Sporty wodne",
                        price = 275
                    },
                    new Product
                    {
                        name = "Kamizelka ratunkowa",
                        description = "Chroni i dodaje uroku",
                        category = "Sporty wodne",
                        price = 48.95m
                    },
                    new Product
                    {
                        name = "Piłka",
                        description = "Zatwierdzone przez FIFA rozmiar i waga",
                        category = "Piłka nożna",
                        price = 19.50m
                    },
                    new Product
                    {
                        name = "Flagi narożne",
                        description = "Nadadzą twojemu boisku profesjonalny wygląd",
                        category = "Piłka nożna",
                        price = 34.95m
                    },
                    new Product
                    {
                        name = "Stadiom",
                        description = "Składany stadion na 35 000 osób",
                        category = "Piłka nożna",
                        price = 79500
                    },
                    new Product
                    {
                        name = "Czapka",
                        description = "Zwiększa efektywność mózgu o 75%",
                        category = "Szachy",
                        price = 16
                    },
                    new Product
                    {
                        name = "Niestabilne krzesło",
                        description = "Zmniejsza szanse przeciwnika",
                        category = "Szachy",
                        price = 29.95m
                    },
                    new Product
                    {
                        name = "Ludzka szachownica",
                        description = "Przyjemna gra dla całej rodziny!",
                        category = "Szachy",
                        price = 75
                    },
                    new Product
                    {
                        name = "Błyszczący król",
                        description = "Figura pokryta złotem i wysadzana diamentami",
                        category = "Szachy",
                        price = 1200
                    }
                );
                context.SaveChanges();
            }
        }

    }
}
