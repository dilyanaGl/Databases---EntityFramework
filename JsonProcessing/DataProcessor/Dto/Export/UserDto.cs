namespace DataProcessor.Dto.Export
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SoldProductDto[] SoldProducts { get; set; }

    }
}