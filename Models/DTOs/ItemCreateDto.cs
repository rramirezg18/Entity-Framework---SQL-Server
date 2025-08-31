namespace HelloApi.Models.DTOs
{
    public class ItemCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }   // lo tienes en el modelo
    }
}
