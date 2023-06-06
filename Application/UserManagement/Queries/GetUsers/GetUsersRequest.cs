using Application.Shared;
using MediatR;

namespace Application.UserManagement.Queries.GetUsers
{
    public class GetUsersRequest : PaginationRequest, IRequest<GetUsersResponse>
    {
    }
}