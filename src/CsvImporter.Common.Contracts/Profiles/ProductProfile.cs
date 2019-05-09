using AutoMapper;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;

namespace CsvImporter.Common.Contracts.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, ProductEntity>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}