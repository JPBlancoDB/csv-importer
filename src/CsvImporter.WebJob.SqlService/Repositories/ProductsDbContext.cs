using CsvImporter.Common.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsvImporter.WebJob.SqlService.Repositories
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<ProductEntity> Products { get; set; }
    }
}