using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGatewaySwagger.Domain;
using PaymentGatewaySwagger.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewaySwagger.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.DbContext>(options =>
             options.UseSqlServer(
       configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<Data.DbContext>();

            services.AddSingleton<IPaymentService, PaymentService>();
        }
    }
}
