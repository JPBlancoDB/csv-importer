using CsvImporter.Common.Utilities;
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
            var connectionString = ConfigurationUtility.GetConfiguration(configuration, "SqlConnectionString");
            
            services.AddDbContext<JobDbContext>(options => options.UseSqlServer(connectionString));            
            services.AddScoped<IJobsRepository, JobsRepository>();
        }
    }
}