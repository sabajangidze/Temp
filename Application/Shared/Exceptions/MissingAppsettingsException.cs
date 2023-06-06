using System.Diagnostics.CodeAnalysis;

namespace Application.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class MissingAppsettingsException : Exception
    {
        public MissingAppsettingsException(string[] messages) : this(messages, null)
        {
        }

        public MissingAppsettingsException(string[] messages, Exception? innerException) : base(string.Join(" ", messages), innerException)
        {
            ConfigurationErrors = messages;
        }

        protected string[] ConfigurationErrors { get; set; }
    }
}