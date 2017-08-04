/*
 * InverseBoolConverterTests.cs
 * Tests for InverseBoolConverter.
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
    public class InverseBoolConverterTests : SimpleConverterTestBase<InverseBoolConverter, bool, bool>
    {
        [Test]
        public void ConvertTest()
        {
            // false case
            ToGuiType(argument: false, expectedResult: true, onTypeFailure: "Returned non-boolean for false.", onValueFailure: "false is not converted to true.");

            // true case
            ToGuiType(argument: true, expectedResult: false, onTypeFailure: "Returned non-boolean for true.", onValueFailure: "true is not converted to false.");

            Assert.Throws<InvalidCastException>(
                code: () => Converter.Convert(value: "blah", targetType: typeof(bool), parameter: null, culture: CultureInfo.CurrentCulture),
                message: "Converted string.");
        }

        [Test]
        public void ConvertBackTest()
        {
            // false case
            ToBackendType(argument: false, expectedResult: true, onTypeFailure: "Returned non-boolean for false.", onValueFailure: "false is not converted to true.");

            // true case
            ToBackendType(argument: true, expectedResult: false, onTypeFailure: "Returned non-boolean for true.", onValueFailure: "true is not converted to false.");

            Assert.Throws<InvalidCastException>(
                code: () => Converter.ConvertBack(value: "blah", targetType: typeof(bool), parameter: null, culture: CultureInfo.CurrentCulture),
                message: "Converted string.");
        }
    }
}
