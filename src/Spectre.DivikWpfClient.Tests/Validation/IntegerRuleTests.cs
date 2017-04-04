using NUnit.Framework;
using Spectre.DivikWpfClient.Validation;
using System.Globalization;

namespace Spectre.Service.Tests
{
    [TestFixture, Category("WpfClient")]
    public class IntegerRuleTests
    {
        [Test]
        public void IntegerRuleValidatesInputProperly()
        {
            var rule = new IntegerRule();
            Assert.IsFalse(rule.Validate("blah", CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(rule.Validate("123.123", CultureInfo.InvariantCulture).IsValid);
            Assert.IsTrue(rule.Validate("123", CultureInfo.InvariantCulture).IsValid);
        }
    }
}
