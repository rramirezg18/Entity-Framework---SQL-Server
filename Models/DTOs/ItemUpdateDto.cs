namespace HelloApi.Models.DTOs
{
    public class ItemUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int UpdatedBy { get; set; }   // lo tienes en el modelo
    }
}
