/*
 * PropertyChangedNotification.cs
 * Base class for ViewModels.
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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Spectre.Mvvm.Base
{
    /// <summary>
    /// Provides handling necessary events when properties are set by user. 
    /// Moreover, provides an error info easy to bind to - validation of input
    /// may be done by means of attributes.
    /// Provides INotifyPropertyChanged implementation along with some
    /// validation, which can be performed by using simple annotations.
    /// Code is present at: http://social.technet.microsoft.com/wiki/contents/articles/22660.data-validation-in-mvvm.aspx
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    public class PropertyChangedNotification : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields

        /// <summary>
        /// The values which are returned by properties.
        /// </summary>
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        #endregion

        #region Protected

        /// <summary>
        /// Sets the value of a property.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="propertySelector">Expression tree contains the property definition.</param>
        /// <param name="value">The property value.</param>
        protected void SetValue<T>(Expression<Func<T>> propertySelector, T value)
        {
            string propertyName = GetPropertyName(propertySelector);

            SetValue<T>(propertyName, value);
        }

        /// <summary>
        /// Sets the value of a property.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The property value.</param>
        protected void SetValue<T>(string propertyName, T value)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Invalid property name", propertyName);
            }

            if (_values.ContainsKey(propertyName) && value != null && value.Equals(_values[propertyName]))
                return;
            if (_values.ContainsKey(propertyName) && _values[propertyName] == null && value == null)
                return;

            _values[propertyName] = value;
            NotifyPropertyChanged(propertyName);
        }

        /// <summary>
        /// Gets the value of a property.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="propertySelector">Expression tree contains the property definition.</param>
        /// <returns>The value of the property or default value if not exist.</returns>
        protected T GetValue<T>(Expression<Func<T>> propertySelector)
        {
            string propertyName = GetPropertyName(propertySelector);

            return GetValue<T>(propertyName);
        }

        /// <summary>
        /// Gets the value of a property.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value of the property or default value if not exist.</returns>
        protected T GetValue<T>(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Invalid property name", propertyName);
            }

            object value;
            if (!_values.TryGetValue(propertyName, out value))
            {
                value = default(T);
                _values.Add(propertyName, value);
            }

            return (T)value;
        }

        /// <summary>
        /// Validates current instance properties using Data Annotations.
        /// </summary>
        /// <param name="propertyName">This instance property to validate.</param>
        /// <returns>Relevant error string on validation failure or <see cref="System.String.Empty"/> on validation success.</returns>
        protected virtual string OnValidate(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Invalid property name", propertyName);
            }

            string error = string.Empty;
            var value = GetValue(propertyName);
            // looks for first property which fails its validation
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>(1);
            var result = Validator.TryValidateProperty(
                value,
                new ValidationContext(this, null, null)
                {
                    MemberName = propertyName
                },
                results);

            if (!result)
            {
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
            }

            return error;
        }

        #endregion

        #region Change Notification

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <typeparam name="T">Type of property which has changed.</typeparam>
        /// <param name="propertySelector">The property selector.</param>
        protected void NotifyPropertyChanged<T>(Expression<Func<T>> propertySelector)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                string propertyName = GetPropertyName(propertySelector);
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // INotifyPropertyChanged Members

        #region Data Validation

        /// <summary>
        /// Gets a value indicating whether this instance has error.
        /// Works through checking whether error message is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </value>
        protected bool HasError => !string.IsNullOrWhiteSpace(((IDataErrorInfo)this).Error);

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        string IDataErrorInfo.Error
        {
            get
            {
                //throw new NotSupportedException("IDataErrorInfo.Error is not supported, use IDataErrorInfo.this[propertyName] instead.");
                var errors = new StringBuilder();
                foreach (var property in _values)
                {
                    var error = ((IDataErrorInfo)this)[property.Key];
                    if (!string.IsNullOrWhiteSpace(error))
                        errors.AppendLine(error);
                }
                return errors.ToString();
            }
        }

        /// <summary>
        /// Gets the error of the property with specified name.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Error info.</returns>
        string IDataErrorInfo.this[string propertyName] => OnValidate(propertyName);

        #endregion

        #region Privates

        /// <summary>
        /// Gets the name of the property from lambda expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Name of the property.</returns>
        /// <exception cref="System.InvalidOperationException">When expression leads to not a member.</exception>
        private string GetPropertyName(LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new InvalidOperationException();
            }

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Gets the value of a property with specific name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Value of the property</returns>
        /// <exception cref="System.ArgumentException">Invalid property name</exception>
        private object GetValue(string propertyName)
        {
            object value;
            if (!_values.TryGetValue(propertyName, out value))
            {
                var propertyDescriptor = TypeDescriptor.GetProperties(GetType()).Find(propertyName, false);
                if (propertyDescriptor == null)
                {
                    throw new ArgumentException("Invalid property name", propertyName);
                }

                value = propertyDescriptor.GetValue(this);
                _values.Add(propertyName, value);
            }

            return value;
        }

        #endregion

        #region Debugging

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real, 
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion // Debugging Aides
    }
}