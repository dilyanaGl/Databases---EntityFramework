namespace DataProcessor.Dto.Export
{
    public class CategoryProductDto
    {
        public string Name { get; set; }
        public int ProductsCount { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}