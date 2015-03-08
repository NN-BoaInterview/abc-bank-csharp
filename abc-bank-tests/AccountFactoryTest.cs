using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountFactoryTest
    {
        [TestMethod]
        public void CanCreateCheckingAccount()
        {
            var account = AccountFactory.Instance.Create(Account.AccountType.Checking);
            Assert.IsInstanceOfType(account, typeof(CheckingAccount));
        }

        [TestMethod]
        public void CanCreateSavingsAccount()
        {
            var account = AccountFactory.Instance.Create(Account.AccountType.Savings);
            Assert.IsInstanceOfType(account, typeof(SavingsAccount));
        }

        [TestMethod]
        public void CanCreateCheckingMaxiSavings()
        {
            var account = AccountFactory.Instance.Create(Account.AccountType.MaxiSavings);
            Assert.IsInstanceOfType(account, typeof(MaxiSavingsAccount));
        }
    }
}
