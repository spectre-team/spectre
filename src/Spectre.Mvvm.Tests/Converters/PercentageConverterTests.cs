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
    [TestFixture, Category("MvvmFramework")]
    public class PercentageConverterTests : SimpleConverterTestBase<PercentageConverter, String, double>
    {
        [Test]
        public void ConvertTest()
        {
            ToGuiType(1.00, "100 %", "Conversion of 1.00 did not return \"100 %\"", "1.00 does not evalute to \"100 %\".");

            ToGuiType(-10.001, "-1000,1 %", "Conversion of -10.001 did not return \"-1000,1 %\"", "-10.001 does not evalute to \"-1000,1 %\".");

            Assert.Throws<InvalidCastException>(
                () => Converter.Convert("blah", typeof(String), null, CultureInfo.CurrentCulture),
                "Converted string.");
        }

        [Test]
        public void ConvertBackTest()
        {
            ToBackendType("99,11 %", 0.9911, "Conversion of \"99,11 %\" did not return 0.9911", "\"99,11 %\" does not evalute to 0.9911.");

            object conversionResult = Converter.ConvertBack(123.0, typeof(double), 1.23, CultureInfo.CurrentCulture);
            Assert.IsInstanceOf(BackendType, conversionResult, "Conversion of 123 did not return 1.23");
            Assert.AreEqual(1.23, (double)conversionResult, "\"123 %\" does not evalute to 1.23.");

            Assert.Throws<InvalidCastException>(
                () => Converter.ConvertBack(12, typeof(double), null, CultureInfo.CurrentCulture),
                "Converted double.");
        }
    }
}
