using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Factories;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddApplicationInsightsTelemetry(Configuration["AppInsightsInstrumentationKey"]);
            
            // For using temp folder in Linux
            // more info: https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core-no-visualstudio#frequently-asked-questions
            services.AddSingleton<ITelemetryChannel>(new ServerTelemetryChannel
            {
                StorageFolder = Configuration["ApplicationInsights:TempFolder"]
            });

            services.AddTransient<IResponseFactory, ResponseFactory>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
