using Domain.Shared;

namespace Domain.Entities
{
    public class Event : BaseEntity<Guid>
    {
        public override Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int ReservedCount { get; set; }
        public int PlaceCount { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}