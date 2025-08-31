namespace HelloApi.Models.DTOs
{
    public class OrderUpdateDto
    {
        public int PersonId { get; set; }   // si NO quieres permitir cambiar la persona, elimina esta propiedad
        public int Number { get; set; }
        public int UpdatedBy { get; set; }
    }
}
