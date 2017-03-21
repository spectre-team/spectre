/*
 * BoolToVisibilityConverter.cs
 * Converter which allows to steer visibility with bools.
 * 
   Copyright 2017 Grzegorz Mrukwa

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
using System.Windows;
using System.Windows.Data;

namespace Spectre.Mvvm.Converters
{
    /// <summary>
	/// This allows to convert <see cref="bool"/> to <see cref="Visibility"/> and back.
	/// </summary>
	/// <seealso cref="System.Windows.Data.IValueConverter" />
	public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type" /> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        /// <exception cref="NullReferenceException">if value is null</exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay" /> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type" /> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        /// <exception cref="NullReferenceException">if value is null</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible;
        }
    }
}
