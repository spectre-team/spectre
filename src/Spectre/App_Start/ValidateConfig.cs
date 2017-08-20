namespace Spectre.App_Start
{
    using System.Configuration;
    using Spectre.Service.Configuration;

    /// <summary>
    /// Provides config validation logic.
    /// </summary>
    public class ValidateConfig
    {
        /// <summary>
        /// Invokes during application startup to validate current configuration.
        /// </summary>
        public static void Validate()
        {
            new DataRootConfig(
                ConfigurationManager.AppSettings["LocalDataDirectory"],
                ConfigurationManager.AppSettings["RemoteDataDirectory"]);
        }
    }
}