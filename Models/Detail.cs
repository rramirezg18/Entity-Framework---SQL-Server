using System.ComponentModel.DataAnnotations.Schema;
namespace HelloApi.Models
{
    public class Detail
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
    }
}