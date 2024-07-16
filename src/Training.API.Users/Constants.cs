using System.Security.Policy;

namespace Training.API.Users
{
    internal static class Constants
    {
        internal const string Serilog = "SerilogLogging";

        internal static class DatabaseSection
        {
            internal const string Root = "Database";
            internal const string Training = Root + ":Users";
        }

        internal static class ExternalApiSection
        {
            internal const string Root = "ExternalApi";
        }

        internal static class WorkerSection
        {
            internal const string Root = "Trigger";
        }

        internal static class KafkaSection
        {
            internal const string Root = "Kafka";
        }

        internal static class AuthenticationSection
        {
            internal const string Root = "Authentication";
            internal const string JwtBearer = Root + ":JwtBearer";
            internal const string InternalServiceJwtBearer = Root + ":InternalServiceJwtBearer";
            internal const string Domain = Root + ":Domain";
            internal const string DefaultConfig = Root + ":DefaultConfig";
        }

        internal static class SwaggerConfigSection
        {
            internal const string Root = "Swagger";
            internal const string SecurityDefinition = Root + ":SecurityDefinition:OAuth2";
            internal const string UiConfig = Root + ":UiConfig";
        }
        internal static class InternalPolicyName
        {
            internal const string PolicyNameInternal = "AuthOrInternalService";
            internal const string ServiceInternal = "ServiceInternal";
        }
    }
}