using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class HelpersTest
    {
        private enum TestEum
        {
            Item1,

            [System.ComponentModel.Description("Item 2")]
            Item2,
        }

        [TestMethod]
        public void CanGetEnumDescriptionWhenNoDescription()
        {
            Assert.AreEqual("Item1", TestEum.Item1.GetDescription());
        }

        [TestMethod]
        public void CanGetEnumDescriptionWhenHaveDescription()
        {
            Assert.AreEqual("Item 2", TestEum.Item2.GetDescription());
        }

        [TestMethod]
        public void CanFormatSingleWord()
        {
            Assert.AreEqual("1 account", 1.FormatWord("account"));
        }

        [TestMethod]
        public void CanFormatPluralWord()
        {
            Assert.AreEqual("2 accounts", 2.FormatWord("account"));
        }

        [TestMethod]
        public void CanFormatDoubleToCurrency()
        {
            Assert.AreEqual("$100.00", 100.0d.ToCurrency());
        }

        [TestMethod]
        public void CanFormatDecimalToCurrency()
        {
            Assert.AreEqual("$100.00", 100.0m.ToCurrency());
        }
    }
}
