namespace HelloApi.Models.DTOs
{
    public class OrderCreateDto
    {
        public int PersonId { get; set; }
        public int Number { get; set; }
        public int CreatedBy { get; set; }
    }
}
