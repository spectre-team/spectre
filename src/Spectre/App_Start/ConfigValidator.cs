/*
 * ConfigValidator.cs
 * Validates application config at startup time.
 *
   Copyright 2017 Maciej Gamrat

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

namespace Spectre.App_Start
{
    using System.Configuration;
    using Spectre.Service.Configuration;

    /// <summary>
    /// Provides config validation logic.
    /// </summary>
    public class ConfigValidator
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