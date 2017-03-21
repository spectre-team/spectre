using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
