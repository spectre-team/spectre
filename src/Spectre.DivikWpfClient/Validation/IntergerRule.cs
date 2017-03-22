/*
 * IntegerRule.cs
 * Contains WPF TextBox integer validation rule.
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
    /// Rule for validating integer input.
    /// </summary>
    public class IntegerRule : ValidationRule
    {
        /// <summary>
		/// Integer input validate method. Checks if passed value is a <see cref="int"/>.
		/// </summary>
		/// <param name="value">The source data being passed to the target.</param>
        /// <param name="cultureInfo">The culture of the conversion.</param>
		/// <returns><see cref="ValidationResult"/> with error messages for the rule.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int num = 0;

            if (!int.TryParse(value.ToString(), out num))
                return new ValidationResult(false, String.Format("Please enter an integer value."));

            return new ValidationResult(true, null);
        }
    }
}
