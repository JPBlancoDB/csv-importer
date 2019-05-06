using CsvImporter.Common.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsvImporter.WebApi.Repositories
{
    public class JobDbContext : DbContext
    {
        public JobDbContext (DbContextOptions<JobDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<JobEntity> Jobs { get; set; }
    }
}