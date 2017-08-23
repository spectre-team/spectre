/*
 * IntPositivenessToBoolConverterTests.cs
 * Tests for IntPositivenessToBoolConverter.
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
    public class
        IntPositivenessToBoolSimpleConverterTests : SimpleConverterTestBase<IntPositivenessToBoolConverter, bool, int>
    {
        [Test]
        public void ConvertTest()
        {
            // 0 case
            ToGuiType(argument: 0, expectedResult: false, onTypeFailure: "Returned non-boolean for 0.", onValueFailure: "0 is converted to true.");

            // positive case
            ToGuiType(argument: 1, expectedResult: true, onTypeFailure: "Returned non-boolean for 1.", onValueFailure: "1 is converted to false.");

            Assert.Throws<InvalidCastException>(
                code: () => Converter.Convert(value: "blah", targetType: typeof(bool), parameter: null, culture: CultureInfo.CurrentCulture),
                message: "Converted string.");
        }

        [Test]
        public void ConvertBackTest()
        {
            Assert.Throws<InvalidOperationException>(
                code: () => Converter.ConvertBack(value: false, targetType: typeof(int), parameter: null, culture: CultureInfo.CurrentCulture),
                message: "Converted bool back.");
        }
    }
}
