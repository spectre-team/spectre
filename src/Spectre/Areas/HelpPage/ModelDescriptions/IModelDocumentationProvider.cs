using System;
using System.Reflection;

namespace Spectre.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Interface of model documentation provider
    /// </summary>
    public interface IModelDocumentationProvider
    {
        /// <summary>
        /// Gets the documentation.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns>documentation for member</returns>
        string GetDocumentation(MemberInfo member);

        /// <summary>
        /// Gets the documentation.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>documentation for type</returns>
        string GetDocumentation(Type type);
    }
}