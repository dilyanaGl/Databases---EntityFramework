using System.ComponentModel.DataAnnotations;

namespace BusTicketSystem.Client.Dto
{
    public class TicketDto
    {
        public int CustomerId { get; set; }

        public int TripId { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        public string Seat { get; set; }
    }
}
