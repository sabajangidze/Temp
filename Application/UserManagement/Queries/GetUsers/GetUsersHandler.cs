using Application.RoleManagement.Dto;
using Application.RoleManagement.Queries.GetRoles;
using Application.UserManagement.Dto;
using Domain.Interfaces.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

namespace Application.UserManagement.Queries.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var users = _userRepository.Query();

            var total = users.Count();

            var usersList = await users.Pagination(request).ToListAsync();

            var response = new GetUsersResponse
            {
                Users = usersList.Select(x => new UserDtoModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                }),
                Page = request.Page,
                PageSize = request.PageSize,
                Total = total,
            };

            return response;
        }
    }
}