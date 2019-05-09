using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;
using CsvImporter.WebJob.SqlService.Abstractions;

namespace CsvImporter.WebJob.SqlService.Repositories
{
    public class ProductsRepository : IProductRepository
    {
        private readonly ProductsDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductsRepository(ProductsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddRange(List<ProductDto> products)
        {
            var productsEntity = _mapper.Map<List<ProductDto>, List<ProductEntity>>(products);

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            await _dbContext.Products.AddRangeAsync(productsEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}