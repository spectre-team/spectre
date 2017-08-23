/*
 * CombinedConverterTests.cs
 * Tests for CombinedConverter.
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
using NUnit.Framework;
using Spectre.Mvvm.Converters;

namespace Spectre.Mvvm.Tests.Converters
{
    [TestFixture]
    [Category(name: "MvvmFramework")]
    public class CombinedConverterTests : SimpleConverterTestBase<CombinedConverter, int, bool>
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            // int -> bool -> !bool conversion
            Converter.First = new IntPositivenessToBoolConverter();
            Converter.Second = new InverseBoolConverter();
        }

        [Test]
        public void ConvertTest()
        {
            var conversionResult = Converter.Convert(value: 1, targetType: typeof(bool), parameter: null, culture: CultureInfo.CurrentCulture);

            Assert.IsInstanceOf(expected: typeof(bool),
                actual: conversionResult,
                message: "Did not convert to proper type.");
            Assert.IsFalse(condition: (bool) conversionResult, message: "Did not convert 1 to false.");

            Assert.Throws<InvalidCastException>(
                code: () => Converter.Convert(value: "blah", targetType: typeof(bool), parameter: null, culture: CultureInfo.CurrentCulture),
                message: "Converted string.");
        }

        [Test]
        public void ConvertBackTest()
        {
            Assert.Throws<InvalidOperationException>(
                code: () => Converter.ConvertBack(value: false, targetType: typeof(int), parameter: null, culture: CultureInfo.CurrentCulture),
                message: "Did not propagate exception.");

            Converter.First = Converter.Second;

            var conversionResult = Converter.ConvertBack(value: true, targetType: typeof(bool), parameter: null, culture: CultureInfo.CurrentCulture);

            Assert.IsInstanceOf(expected: typeof(bool),
                actual: conversionResult,
                message: "Converted true to unknown type.");
            Assert.IsTrue(condition: (bool) conversionResult);
        }
    }
}
