using Application.Shared.Behaviors;
using Application.Shared.Configurations;
using Application.Shared.Configurations.Validators;
using Application.Shared.Exceptions;
using Application.Shared.Options;
using Application.UserManagement.Commands.CreateUser;
using Domain.Entities;
using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repository;
using Infrastructure.DataAccess;
using Infrastructure.Repositories;
using Shared.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using MediatR;
using System.Reflection;
using System.Text;

namespace DI
{
    public class DependencyResolver
    {
        public IConfiguration Configuration { get; }

        public DependencyResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceCollection Resolve(IServiceCollection services)
        {
            if (services == null)
            {
                services = new ServiceCollection();
            }

            var appsettings = new AppSettings();
            Configuration.Bind(appsettings);
            ValidateConfiguration(appsettings);

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("Default")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMediatR(new[]
            {
                 typeof(CreateUser).GetTypeInfo().Assembly,
            });

            services.AddValidatorsFromAssembly(typeof(CreateUserValidator).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            ConfigureIdentity(services);
            ConfgiureJwt(services, Configuration);
            ConfigurePolicies(services);

            return services;
        }

        public static void ConfigureIdentity(IServiceCollection services)
        {
            var builder = services.AddIdentity<User, Role>(x =>
            {
                x.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfgiureJwt(IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(JwtOptions.ConfigSection).Get<JwtOptions>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Audience = jwtOptions!.ValidAudience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.ValidIssuer,
                    ValidAudiences = jwtOptions.ValidAudiences,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                };
            });
        }

        public static void ConfigurePolicies(IServiceCollection services)
        {
            services.AddAuthorization(option =>
            {
                option.AddPolicy(RoleClaims.Event_Create, policy => policy.RequireClaim("roleClaims", RoleClaims.Event_Create));
                option.AddPolicy(RoleClaims.Event_Update, policy => policy.RequireClaim("roleClaims", RoleClaims.Event_Update));
                option.AddPolicy(RoleClaims.Event_Delete, policy => policy.RequireClaim("roleClaims", RoleClaims.Event_Delete));
            });
        }

        internal static void ValidateConfiguration(AppSettings appSettings)
        {
            var validator = new AppSettingsValidator();
            var validationResult = validator.Validate(appSettings);
            if (!validationResult.IsValid)
            {
                throw new MissingAppsettingsException(validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }
        }
    }
}