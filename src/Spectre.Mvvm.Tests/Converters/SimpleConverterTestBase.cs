/*
 * SimpleConverterTestBase.cs
 * Base class for converters' tests.
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
using System.Globalization;
using System.Windows.Data;
using NUnit.Framework;
using Spectre.Mvvm.Converters;

namespace Spectre.Mvvm.Tests.Converters
{
    [TestFixture]
    [Category(name: "MvvmFramework")]
    public abstract class SimpleConverterTestBase<T, TGui, TBackend>
        where T : IValueConverter, new()
    {
        protected T Converter;
        protected Type GuiType;
        protected Type BackendType;

        [SetUp]
        public virtual void SetUp()
        {
            Converter = new T();
            GuiType = typeof(TGui);
            BackendType = typeof(TBackend);
        }

        protected virtual void ToGuiType(
            TBackend argument,
            TGui expectedResult,
            string onTypeFailure,
            string onValueFailure)
        {
            var conversionResult = Converter.Convert(argument,
                GuiType,
                parameter: null,
                culture: CultureInfo.InvariantCulture);
            Assert.IsInstanceOf(GuiType, conversionResult, onTypeFailure);
            Assert.AreEqual(expectedResult, actual: (TGui) conversionResult, message: onValueFailure);
        }

        protected virtual void ToBackendType(
            TGui argument,
            TBackend expectedResult,
            string onTypeFailure,
            string onValueFailure)
        {
            var conversionResult = Converter.ConvertBack(argument,
                BackendType,
                parameter: null,
                culture: CultureInfo.InvariantCulture);
            Assert.IsInstanceOf(BackendType, conversionResult, onTypeFailure);
            Assert.AreEqual(expectedResult, actual: (TBackend) conversionResult, message: onValueFailure);
        }
    }
}
