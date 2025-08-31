namespace HelloApi.Models.DTOs
{
    public class DetailCreateDto
    {
        public int StatusId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ItemId { get; set; }
    }
}
