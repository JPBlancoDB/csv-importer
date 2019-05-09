using CsvHelper.Configuration.Attributes;

namespace CsvImporter.Common.Contracts.DTOs
{
    public class ProductDto
    {
        public string Key { get; set; }

        [Name("ArtikelCode")]
        public string ArticleCode { get; set; }

        public string ColorCode { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountPrice { get; set; }

        public string DeliveredIn { get; set; }

        public string Q1 { get; set; }

        public int Size { get; set; }

        public string Color { get; set; }
    }
}