using Buyit.DAL.Services.Implementations;
using Buyit.DAL.Services.Interfaces;
using Buyit.DAL.Models.Cart;
using Ecommerce_Web_API.Installers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buyit.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void RegisterServices(IServiceCollection services, IConfiguration Configuration)
        {


            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => Buyit.DAL.Models.Cart.ShoppingCart.GetCart(sp));


        }
    }
}
