using Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Role : IdentityRole, IBaseEntity<string>
    {
        public string? Description { get; set; }
    }
}