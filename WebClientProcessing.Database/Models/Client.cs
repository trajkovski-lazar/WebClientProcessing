using System.ComponentModel.DataAnnotations;

namespace WebClientProcessing.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
