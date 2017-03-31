/*
 * IntegerRule.cs
 * Contains WPF TextBox percentage validation rule.
 * 
   Copyright 2017 Michał Wolny

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
using System;
using System.Globalization;
using System.Windows.Controls;

namespace Spectre.DivikWpfClient.Validation
{
    /// <summary>
    /// Rule for validating percentage input.
    /// </summary>
    public class PercentageRangeRule : ValidationRule
    {
        /// <summary>
        /// Minmum allowed value.
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// Maximum allowed value.
        /// </summary>
        public double Max { get; set; }

        /// <summary>
		/// Percentage input validate method. Checks if passed value is a <see cref="double"/>
        /// within given range, accepts trailing '%' sign. 
		/// </summary>
		/// <param name="value">The source data being passed to the target.</param>
        /// <param name="cultureInfo">The culture of the conversion.</param>
		/// <returns><see cref="ValidationResult"/> with error messages for the rule.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double parameter = 0;

            try
            {
                if (((string)value).Length > 0)
                {
                    parameter = double.Parse(((string)value).Replace('%', ' ').Trim());
                } else
                {
                    return new ValidationResult(false, "Please enter a valid percentage value.");
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Please enter a valid percentage value.");
            }

            if ((parameter < this.Min) || (parameter > this.Max))
            {
                return new ValidationResult(false, "Please enter percentage value in the range: " + this.Min + " - " + this.Max + ".");
            }
            return new ValidationResult(true, null);
        }
    }
}
