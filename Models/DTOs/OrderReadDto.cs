namespace HelloApi.Models.DTOs
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string? PersonFullName { get; set; }
        public int Number { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
