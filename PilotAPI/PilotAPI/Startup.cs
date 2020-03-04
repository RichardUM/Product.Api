using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PilotAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PilotAPI.Services;
using Swashbuckle.AspNetCore;

namespace PilotAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddXmlSerializerFormatters();
            //services.AddApiVersioning();
            //services.AddApiVersioning(o => o.ApiVersionReader = new MediaTypeApiVersionReader());
            services.AddDbContext<ProductDbContext>(option => option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductDB;"));

            //Responsible for resolving dependency and object creation
            services.AddScoped<IProducts, ProductRepository>();

            //Resolve dependencies
            services.AddSwaggerGen(c => c.SwaggerDoc("myApi", new OpenApiInfo {Title = "Products API",Version = "2.0"}));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ProductDbContext productDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/myApi/swagger.json", "Api for Products"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            productDbContext.Database.EnsureCreated();
        }
    }
}
