#nullable disable

using Application.Shared;
using Application.UserManagement.Dto;

namespace Application.UserManagement.Queries.GetUsers
{
    public class GetUsersResponse : PaginationResponse
    {
        public IEnumerable<UserDtoModel> Users { get; set; }
    }
}