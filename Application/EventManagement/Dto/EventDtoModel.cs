#nullable disable

using Application.UserManagement.Dto;

namespace Application.EventManagement.Dto
{
    public class EventDtoModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int ReservedCount { get; set; }
        public int PlaceCount { get; set; }
        public string UserId { get; set; }
    }
}