using Application.Shared.Exceptions;
using Domain.Entities;
using Domain.Interfaces.Repository;
using FluentValidation.Results;
using MediatR;

namespace Application.UserManagement.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public CreateUserHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.OfIdAsync(request.RoleId.ToString());

            if (role == null)
            {
                throw new KeyNotFoundException($"Role was not found for Id: {request.RoleId}");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userRepository.InsertAsync(user, request.Password, role.Name!);

            if (!result.Succeeded)
            {
                var validationFailures = result.Errors.Select(x => new ValidationFailure
                {
                    ErrorMessage = x.Description,
                    ErrorCode = x.Code,
                }).ToList();

                throw new ValidationException(validationFailures);
            }

            return Unit.Value;
        }
    }
}