using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationWsei.Models;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using WebApplication9.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using Swashbuckle.Swagger;

namespace WebApplicationWsei
{
    public class ElapsedTimeMiddleware
    {
        private readonly ILogger _logger;
        RequestDelegate _next;
        public ElapsedTimeMiddleware(RequestDelegate next, ILogger<ElapsedTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            await _next(context);
            var isHtml = context.Response.ContentType?.ToLower().Contains("text/html");
            if (context.Response.StatusCode == 200 && isHtml.GetValueOrDefault())
            {
                _logger.LogInformation($"{context.Request.Path} executed in {sw.ElapsedMilliseconds}ms");
            }
        }
    }

    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseElapsedTimeMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ElapsedTimeMiddleware>();
        }
    }

    public class Startup
    {

        public Startup(IConfiguration configuration) =>
        Configuration = configuration;

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, EFProductRepository>();

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            services.AddRazorPages();

        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)

        {

          
          
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages(); // strony ze statusem b³êdu

            app.UseStaticFiles(); // ob³uga css, images, js
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();
            app.UseElapsedTimeMiddleware();







            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute(
            name: "default",
                pattern: "{controller=Product}/{action=List}");
                endpoints.MapControllerRoute(
                    name: "productList",
                    pattern: "{controller=Product}/{action=List}/{category}");
                endpoints.MapControllerRoute(
            name: "adminView",
                pattern: "{controller=Admin}/{action=Index}");
            }
            );

            SeedData.EnsurePopulated(app);


        }
    }

  
}
