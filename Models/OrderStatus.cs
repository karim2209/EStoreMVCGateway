using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EStoreMVCGateway.Models
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required, MaxLength(50)]
        public string? StatusName { get; set; }
    }
}