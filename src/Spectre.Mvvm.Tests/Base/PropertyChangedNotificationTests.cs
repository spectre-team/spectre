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
    [TestFixture, NUnit.Framework.Category("MvvmFramework")]
    public class PropertyChangedNotificationTests
    {
        #region AnyVm
        private class AnyVm : PropertyChangedNotification
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage)]
            public string String
            {
                get { return GetValue(() => String); }
                set { SetValue(() => String, value); }
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
            int n = 0;
            _any.PropertyChanged += (obj, e) => ++n;
            _any.String = "Blah";

            Assert.AreEqual(1, n, "Event not fired.");

            string propertyName = string.Empty;
            _any.PropertyChanged += (obj, e) => propertyName = e.PropertyName;
            _any.String = "Another blah";

            Assert.AreEqual("String", propertyName, "Wrong property name used.");

            n = 0;
            _any.String = _any.String;

            Assert.AreEqual(0, n, "Fired update event int the case of equal input.");
        }
        #endregion

        #region TestErrorInfo
        [Test]
        public void TestErrorInfo()
        {
            //a value has to be created (it is created on first read)
            var garbage = _any.String;

            Assert.AreEqual(ErrorMessage, (_any as IDataErrorInfo).Error.Trim(),
                "Error message differs");

            _any.String = "Blah";

            Assert.AreEqual(string.Empty, (_any as IDataErrorInfo).Error,
                "Error message without an error");
        }
        #endregion

        #region TestIndexer

        [Test]
        public void TestIndexer()
        {
            //a value has to be created (it is created on first read)
            var garbage = _any.String;

            Assert.AreEqual(ErrorMessage, (_any as IDataErrorInfo)["String"].Trim(),
                "Error message differs");

            _any.String = "Blah";

            Assert.AreEqual(string.Empty, (_any as IDataErrorInfo)["String"],
                "Error message without an error");
        }
        #endregion
    }
}
