namespace DataProcessor.Dto.Export
{
    public class CarPartDto
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public long TravelledDistance { get; set; }
        public PartCarDto[] Parts { get; set; }
    }
}