namespace DataProcessor.Dto.Export
{
    public class ProductSoldDto 
    {
        public int Count { get; set; }
        public ProductExportDto[] Products { get; set; }
    }
}