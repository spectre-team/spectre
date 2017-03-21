/*
 * AnyConverterThrowTests.cs
 * Tests for any converter that it throws NullReferenceException when supplied with null.
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
using System.Windows;
using System.Windows.Data;
using NUnit.Framework;
using Spectre.Mvvm.Converters;

namespace Spectre.Mvvm.Tests.Converters
{
    [TestFixture]
    public class AnyConverterThrowTests
    {
        public static IEnumerable<ConverterTestCase> TwoWayConverters
        {
            get
            {
                yield return new ConverterTestCase(new BoolToVisibilityConverter(), typeof(Visibility), typeof(bool));
                yield return new ConverterTestCase(new CombinedConverter(), typeof(int), typeof(bool));
                yield return new ConverterTestCase(new InverseBoolConverter(), typeof(bool), typeof(bool));
            }
        }

        public static IEnumerable<ConverterTestCase> ForwardOnlyConverters
        {
            get
            {
                yield return new ConverterTestCase(new IntPositivenessToBoolConverter(), typeof(int), typeof(bool));
            }
        }

        public static IEnumerable<ConverterTestCase> AllConverters
        {
            get
            {
                foreach (var tc in TwoWayConverters)
                    yield return tc;
                foreach (var tc in ForwardOnlyConverters)
                    yield return tc;
            }
        }

        [Test, TestCaseSource("AllConverters")]
        public virtual void ThrowsOnNullArgumentTest(ConverterTestCase convCase)
        {
            Assert.Throws<NullReferenceException>(
                () => convCase.Converter.Convert(null, convCase.GuiType, null, CultureInfo.CurrentCulture),
                "Converted null without an exception.");
        }

        [Test, TestCaseSource("TwoWayConverters")]
        public virtual void ThrowsOnNullArgumentBackTest(ConverterTestCase convCase)
        {
            Assert.Throws<NullReferenceException>(
                () => convCase.Converter.ConvertBack(null, convCase.BackendType, null, CultureInfo.CurrentCulture),
                "Converted back null without an exception.");
        }

        public class ConverterTestCase
        {
            public IValueConverter Converter { get; }
            public Type GuiType { get; }
            public Type BackendType { get; }

            public ConverterTestCase(IValueConverter converter, Type gui, Type backend)
            {
                Converter = converter;
                GuiType = gui;
                BackendType = backend;
            }
        }
    }
}
