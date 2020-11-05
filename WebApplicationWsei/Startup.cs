using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplicationWsei.Models;
using WebApplication9.Models;
using Microsoft.AspNetCore.Routing.Matching;

namespace WebApplicationWsei
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
        Configuration = configuration;

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)

        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseDeveloperExceptionPage(); // info o b��dach
            app.UseStatusCodePages(); // strony ze statusem b��du
            app.UseStaticFiles(); // ob�uga css, images, js

            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute(
            name: "default",
                pattern: "{controller=Product}/{action=List}");
                endpoints.MapControllerRoute(
                    name: "productList",
                    pattern: "{controller=Product}/{action=List}/{category}");
                    });

            SeedData.EnsurePopulated(app);


        }
    }
}
