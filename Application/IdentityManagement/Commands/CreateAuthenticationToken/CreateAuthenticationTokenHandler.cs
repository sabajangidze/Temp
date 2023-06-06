using Domain.Entities;
using Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.IdentityManagement.Commands.CreateAuthenticationToken
{
    public class CreateAuthenticationTokenHandler : IRequestHandler<CreateAuthenticationTokenRequest, CreateAuthenticationTokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleClaimRepository _roleClaimRepository;
        private readonly IConfiguration _configuration;

        public CreateAuthenticationTokenHandler(IUserRepository userRepository, IRoleRepository roleRepository, IRoleClaimRepository roleClaimRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _roleClaimRepository = roleClaimRepository;
            _configuration = configuration;
        }

        public async Task<CreateAuthenticationTokenResponse> Handle(CreateAuthenticationTokenRequest request, CancellationToken cancellationToken)
        {
            var validateUser = await _userRepository.ValidateUserAsync(request.Username, request.Password);

            if (validateUser.success)
            {
                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims(validateUser.user!);
                var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return new CreateAuthenticationTokenResponse(true, token);
            }

            return new CreateAuthenticationTokenResponse(false, string.Empty);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _configuration.GetSection("JwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["SecretKey"]!);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(UserClaims.Username, user.UserName!),
                new Claim(UserClaims.UserId, user.Id),
            };

            var roleName = (await _userRepository.GetUserRolesAsync(user)).First();

            claims.Add(new Claim(UserClaims.Role, roleName));

            var role = await _roleRepository.FindByNameAsync(roleName);

            var roleClaims = await _roleClaimRepository.GetRoleClaimsAsync(role);

            foreach (var roleClaim in roleClaims)
            {
                claims.Add(new Claim(UserClaims.RoleClaims, roleClaim.Type));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtConfig");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["ValidIssuer"],
                audience: jwtSettings["ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}