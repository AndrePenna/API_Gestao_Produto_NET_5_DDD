using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Generic;
using Domain.Interfaces.Services;
using Domain.Services;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generic;
using Infrastructure.Repository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI
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
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<ContextBase>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "WebAPI", Version = "v2" });
            });

            services.AddSingleton(typeof(IGeneric<>), typeof(GenericRepository<>));
            services.AddSingleton<IProduct, ProductRepository>();
            services.AddSingleton<ISupplier, SupplierRepository>();

            services.AddSingleton<IProductServices, ProductServices>();
            services.AddSingleton<ISupplierServices, SupplierServices>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductViewModel, Product>();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<SupplierViewModel, Supplier>();
                cfg.CreateMap<Supplier, SupplierViewModel>();
                cfg.CreateMap<ProductSupplierViewModel, Product>();
                cfg.CreateMap<Product, ProductSupplierViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var urlCliente = "http://localhost:4200";
            app.UseCors(x => x
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader().WithOrigins(urlCliente));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPI v2"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
