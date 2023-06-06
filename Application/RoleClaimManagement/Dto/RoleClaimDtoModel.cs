#nullable disable

namespace Application.RoleClaimManagement.Dto
{
    public class RoleClaimDtoModel
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}