using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void TransactionIsCorrectType()
        {
            var transaction = new Transaction(5);
            Assert.IsInstanceOfType(transaction, typeof(Transaction));
        }

        [TestMethod]
        public void CanGetCorrectTransactionDate()
        {
            var transaction = new Transaction(5);
            Assert.IsTrue((DateTime.Now - transaction.Date) <= new TimeSpan(0, 0, 1));
        }
    }
}
