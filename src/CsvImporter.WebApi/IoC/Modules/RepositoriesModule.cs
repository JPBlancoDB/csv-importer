using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebApi.IoC.Modules
{
    public static class RepositoriesModule
    {
        public static void Load(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JobDbContext>(options => options.UseSqlServer(configuration["SqlConnectionString"]));            
            services.AddScoped<IJobsRepository, JobsRepository>();
        }
    }
}