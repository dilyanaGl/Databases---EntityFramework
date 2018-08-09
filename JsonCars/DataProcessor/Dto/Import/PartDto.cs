using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DataProcessor.Dto.Import
{
    public class PartDto
    {
        [Required]
        public string Name { get; set; }


        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }


    }
}