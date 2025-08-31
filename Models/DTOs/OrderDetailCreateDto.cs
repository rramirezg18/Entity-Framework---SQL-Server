namespace HelloApi.Models.DTOs
{
    public class OrderDetailCreateDto
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
    }
}
