namespace HelloApi.Models.DTOs
{
    public class DetailReadDto
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int ItemId { get; set; }
        public string? ItemName { get; set; }
    }
}
