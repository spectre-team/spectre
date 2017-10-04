/*
 * PropertyChangedNotificationTests.cs
 * Tests of PropertyChangedNotification.
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

using System.ComponentModel;
using NUnit.Framework;
using Spectre.Mvvm.Base;
using System.ComponentModel.DataAnnotations;

namespace Spectre.Mvvm.Tests.Base
{
    [TestFixture]
    [NUnit.Framework.Category(name: "MvvmFramework")]
    public class PropertyChangedNotificationTests
    {
        #region AnyVm

        private class AnyVm : PropertyChangedNotification
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = PropertyChangedNotificationTests.ErrorMessage)]
            public string String
            {
                get { return GetValue(propertySelector: () => String); }
                set { SetValue(propertySelector: () => String, value: value); }
            }
        }

        #endregion

        #region Fields

        private AnyVm _any;
        private const string ErrorMessage = "Cannot be empty";

        #endregion

        #region SetUp

        [SetUp]
        public void SetUp()
        {
            _any = new AnyVm();
        }

        #endregion

        #region TestNotification

        [Test]
        public void TestNotification()
        {
            var n = 0;
            _any.PropertyChanged += (obj, e) => ++n;
            _any.String = "Blah";

            Assert.AreEqual(expected: 1, actual: n, message: "Event not fired.");

            var propertyName = string.Empty;
            _any.PropertyChanged += (obj, e) => propertyName = e.PropertyName;
            _any.String = "Another blah";

            Assert.AreEqual(expected: "String", actual: propertyName, message: "Wrong property name used.");

            n = 0;
            _any.String = _any.String;

            Assert.AreEqual(expected: 0, actual: n, message: "Fired update event int the case of equal input.");
        }

        #endregion

        #region TestErrorInfo

        [Test]
        public void TestErrorInfo()
        {
            //a value has to be created (it is created on first read)
            var garbage = _any.String;

            Assert.AreEqual(PropertyChangedNotificationTests.ErrorMessage,
                actual: (_any as IDataErrorInfo).Error.Trim(),
                message: "Error message differs");

            _any.String = "Blah";

            Assert.AreEqual(string.Empty,
                (_any as IDataErrorInfo).Error,
                message: "Error message without an error");
        }

        #endregion

        #region TestIndexer

        [Test]
        public void TestIndexer()
        {
            //a value has to be created (it is created on first read)
            var garbage = _any.String;

            Assert.AreEqual(PropertyChangedNotificationTests.ErrorMessage,
                actual: (_any as IDataErrorInfo)[columnName: "String"]
                .Trim(),
                message: "Error message differs");

            _any.String = "Blah";

            Assert.AreEqual(string.Empty,
                actual: (_any as IDataErrorInfo)[columnName: "String"],
                message: "Error message without an error");
        }

        #endregion
    }
}
