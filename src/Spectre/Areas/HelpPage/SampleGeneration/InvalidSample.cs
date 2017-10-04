using System;

namespace Spectre.Areas.HelpPage
{
    /// <summary>
    /// This represents an invalid sample on the help page. There's a display template named InvalidSample associated with this class.
    /// </summary>
    public class InvalidSample
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSample"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <exception cref="System.ArgumentNullException">errorMessage</exception>
        public InvalidSample(string errorMessage)
        {
            if (errorMessage == null)
            {
                throw new ArgumentNullException("errorMessage");
            }
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; private set; }

        /// <inheritdoc cref="object"/>
        public override bool Equals(object obj)
        {
            InvalidSample other = obj as InvalidSample;
            return other != null && ErrorMessage == other.ErrorMessage;
        }

        /// <inheritdoc cref="object"/>
        public override int GetHashCode()
        {
            return ErrorMessage.GetHashCode();
        }

        /// <inheritdoc cref="object"/>
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}