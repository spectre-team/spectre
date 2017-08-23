using System.Web.Http;
using System.Web.Mvc;

namespace Spectre.Areas.HelpPage
{
    /// <summary>
    /// Registers content of the help page
    /// </summary>
    /// <seealso cref="System.Web.Mvc.AreaRegistration" />
    public class HelpPageAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets the name of the area to register to.
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}