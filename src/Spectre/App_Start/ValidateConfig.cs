using Spectre.Service.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Spectre.App_Start
{
    public class ValidateConfig
    {
        public static void Validate()
        {
            new DataRootConfig(ConfigurationManager.AppSettings["LocalDataDirectory"],
                ConfigurationManager.AppSettings["RemoteDataDirectory"]);
        }
    }
}