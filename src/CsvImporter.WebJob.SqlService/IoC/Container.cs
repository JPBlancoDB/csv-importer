using AutoMapper;
using CsvImporter.Common.Contracts.Profiles;
using CsvImporter.Common.Utilities.IoC;
using CsvImporter.WebJob.SqlService.Abstractions;
using CsvImporter.WebJob.SqlService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebJob.SqlService.IoC
{
    public class Container
    {
        public static void Load(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ProductProfile));
            
            var connectionString = configuration["SqlConnectionString"];
            services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer(connectionString));            
            services.AddScoped<IProductRepository, ProductsRepository>();
            CommonModule.Load(services);
        }
    }
}