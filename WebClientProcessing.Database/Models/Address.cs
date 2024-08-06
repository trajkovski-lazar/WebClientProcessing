using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebClientProcessing.Models
{
    public enum AddressType
    {
        Home = 1,
        Vacation = 2
    }
    public class Address
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public AddressType Type { get; set; }
        [StringLength(100)]
        [RegularExpression(@"^[\w\s\d,]+$", ErrorMessage = "Invalid address format.")]
        public string Value { get; set; }
        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
