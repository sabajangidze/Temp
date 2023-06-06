#nullable disable

using Application.RoleManagement.Dto;

namespace Application.UserManagement.Dto
{
    public class UserDtoModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}