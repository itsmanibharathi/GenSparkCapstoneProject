using api.Models.Enums;

namespace api.Models.Dtos.UserPropertyInteractionDto
{
    public class ReturnUserPropertyInteractionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public PropertyInteractionsType InteractionType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
