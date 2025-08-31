namespace HelloApi.Models.DTOs
{
    public class OrderDetailUpdateDto
    {
        public int ItemId { get; set; }  
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int UpdatedBy { get; set; }
    }
}
