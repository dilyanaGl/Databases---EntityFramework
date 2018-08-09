using DataProcessor.Dto.Export;

namespace DataProcessor
{
    internal class UsersProductDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public ProductSoldDto SoldProducts { get; set; }
    }
}