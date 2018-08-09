namespace DataProcessor.Dto.Export
{
    internal class TotalUserDto
    {
        public int Count { get; set; }
        public UsersProductDto[] Users { get; set; }
    }
}