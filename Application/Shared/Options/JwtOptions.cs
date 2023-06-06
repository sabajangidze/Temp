using System.ComponentModel.DataAnnotations;

namespace Application.Shared.Options
{
    public sealed class JwtOptions
    {
        public const string ConfigSection = "JwtConfig";

        [Required]
        public string ValidIssuer { get; set; } = default!;

        [Required]
        public string ValidAudience { get; set; } = default!;

        public string[] ValidAudiences { get; set; } = Array.Empty<string>();

        [Required]
        public string SecretKey { get; set; } = default!;

        public int ExpiresInMinutes { get; set; } = 60;
    }
}