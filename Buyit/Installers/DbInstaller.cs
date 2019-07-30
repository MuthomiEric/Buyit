using System;
using Buyit.BOL.DTO.Users;
using Buyit.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce_Web_API.Installers
{
    public class DbInstaller : IInstaller
    {
        public void RegisterServices(IServiceCollection services, IConfiguration Configuration)
        {


            #region SQL INSTALLER
            services.AddDbContext<BuyitDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BuyitCS")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                //options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })

                .AddEntityFrameworkStores<BuyitDbContext>()
                .AddDefaultTokenProviders();
            #endregion
        }

    }
}
