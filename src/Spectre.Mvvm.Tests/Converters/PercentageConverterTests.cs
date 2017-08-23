/*
 * PercentageConverterTests.cs
 * Tests for PercentageConverter.
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
using System.Windows;
using NUnit.Framework;
using Spectre.Mvvm.Converters;

namespace Spectre.Mvvm.Tests.Converters
{
    [TestFixture]
    [Category(name: "MvvmFramework")]
    public class PercentageConverterTests : SimpleConverterTestBase<PercentageConverter, string, double>
    {
        [Test]
        public void ConvertTest()
        {
            ToGuiType(argument: 1.00,
                expectedResult: "100 %",
                onTypeFailure: "Conversion of 1.00 did not return \"100 %\"",
                onValueFailure: "1.00 does not evalute to \"100 %\".");

            ToGuiType(argument: -10.001,
                expectedResult: "-1000.1 %",
                onTypeFailure: "Conversion of -10.001 did not return \"-1000.1 %\"",
                onValueFailure: "-10.001 does not evalute to \"-1000.1 %\".");

            Assert.Throws<InvalidCastException>(
                code: () => Converter.Convert(value: "blah", targetType: typeof(string), parameter: null, culture: CultureInfo.CurrentCulture),
                message: "Converted string.");
        }

        [Test]
        public void ConvertBackTest()
        {
            ToBackendType(argument: "99.11 %",
                expectedResult: 0.9911,
                onTypeFailure: "Conversion of \"99.11 %\" did not return 0.9911",
                onValueFailure: "\"99.11 %\" does not evalute to 0.9911.");

            var conversionResult = Converter.ConvertBack(value: 123.0, targetType: typeof(double), parameter: 1.23, culture: CultureInfo.CurrentCulture);
            Assert.IsInstanceOf(BackendType, conversionResult, message: "Conversion of 123 did not return 1.23");
            Assert.AreEqual(expected: 1.23,
                actual: (double) conversionResult,
                message: "\"123 %\" does not evalute to 1.23.");

            Assert.Throws<InvalidCastException>(
                code: () => Converter.ConvertBack(value: 12, targetType: typeof(double), parameter: null, culture: CultureInfo.CurrentCulture),
                message: "Converted double.");
        }
    }
}
