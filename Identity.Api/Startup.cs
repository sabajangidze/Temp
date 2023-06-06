using Application.Shared.Middlewares;
using DI;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

namespace Identity.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            new DependencyResolver(Configuration).Resolve(services);

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            ConfigureSwagger(services);
            services.AddSwaggerGen();

            services.Configure<ForwardedHeadersOptions>(o =>
            {
                o.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                o.KnownNetworks.Clear();
                o.KnownProxies.Clear();
            });

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAnyOrigin");
            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.DefaultModelExpandDepth(-1));

            app.UseForwardedHeaders();
            app.UseCertificateForwarding();

            app.UseRouting();
            app.UseHealthChecks("/health");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=index}/{id?}");
            });

            app.UseMiddleware<ErrorHandlerMiddleware>();
        }

        public static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Identity Api",
                    Version = "v1",
                    Description = "Identity Api",
                    Contact = new OpenApiContact
                    {
                        Name = "Identity Api",
                    },
                });
                c.ResolveConflictingActions(apiDescription => apiDescription.First());
            });
        }
    }
}