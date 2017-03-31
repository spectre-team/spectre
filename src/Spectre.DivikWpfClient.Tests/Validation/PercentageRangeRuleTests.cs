using NUnit.Framework;
using Spectre.DivikWpfClient.Validation;
using System.Globalization;

namespace Spectre.Service.Tests
{
    [TestFixture, Category("WpfClient")]
    public class PercentageRangeRuleTests
    {
        [Test]
        public void PercentageRangeRuleValidatesInputProperly()
        {
            var rule = new PercentageRangeRule();
            rule.Max = 100;
            rule.Min = 0;
            Assert.IsFalse(rule.Validate("", CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(rule.Validate("blah", CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(rule.Validate("-11,12", CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(rule.Validate("123 %", CultureInfo.InvariantCulture).IsValid);
            Assert.IsTrue(rule.Validate("99,99", CultureInfo.InvariantCulture).IsValid);
            Assert.IsTrue(rule.Validate("0 %", CultureInfo.InvariantCulture).IsValid);
        }
    }
}
