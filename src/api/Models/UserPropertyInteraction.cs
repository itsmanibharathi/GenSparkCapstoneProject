using api.Models.Enums;

namespace api.Models
{
    public class UserPropertyInteraction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public PropertyInteractionsType InteractionType { get; set; }
        public DateTime CreatedAt { get; set; }


        // Navigation properties
        public User User { get; set; }
        public Property Property { get; set; }
    }
}
