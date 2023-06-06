using Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser, IBaseEntity<string>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<Event>? Events { get; set; }
    }
}