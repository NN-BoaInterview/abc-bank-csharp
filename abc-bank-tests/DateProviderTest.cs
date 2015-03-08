using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class DateProviderTest
    {
        [TestMethod]
        public void DateProviderIsCorrectType()
        {
            var dateProvider = DateProvider.Instance;
            Assert.IsInstanceOfType(dateProvider, typeof(DateProvider));
        }

        [TestMethod]
        public void CanGetCurrentDate()
        {
            // We accept 1 second difference
            Assert.IsTrue((DateTime.Now - DateProvider.Instance.Now()) <= new TimeSpan(0, 0, 1));
        }
    }
}
