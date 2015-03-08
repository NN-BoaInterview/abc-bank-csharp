using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        // Round interest rate to the nearest 2 digits (cents)
        private const Int32 RoundingDigits = 2;
        private const Double DoubleDelta = 1e-15;

        [TestMethod]
        public void CalculateDailyInterestForCheckingAccountYear()
        {
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.Checking);
            DateProvider.Instance.Now = () => DateTime.Now.AddYears(-1);
            account.Deposit(1000);
            DateProvider.Instance.Now = () => DateTime.Now;
            account.Withdraw(1000);
            var interest = account.InterestEarned();
            Assert.AreEqual(1.00m, Math.Round(interest, RoundingDigits));
        }

        [TestMethod]
        public void CalculateDailyInterestForCheckingAccountMonth()
        {
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.Checking);
            DateProvider.Instance.Now = () => DateTime.Now.AddDays(-30);
            account.Deposit(1000);
            DateProvider.Instance.Now = () => DateTime.Now;
            var interest = account.InterestEarned();
            Assert.AreEqual(0.08m, Math.Round(interest, RoundingDigits));
        }

        [TestMethod]
        public void CalculateDailyInterestForSavingsAccountYear()
        {
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.Savings);
            DateProvider.Instance.Now = () => DateTime.Now.AddYears(-1);
            account.Deposit(2000);
            DateProvider.Instance.Now = () => DateTime.Now;
            var interest = account.InterestEarned();
            Assert.AreEqual(3.00m, Math.Round(interest, RoundingDigits));
        }

        [TestMethod]
        public void CalculateDailyInterestForSavingsAccountMonthForThousandDollar()
        {
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.Savings);
            DateProvider.Instance.Now = () => DateTime.Now.AddDays(-30);
            account.Deposit(1000);
            DateProvider.Instance.Now = () => DateTime.Now;
            var interest = account.InterestEarned();
            // Should be the same result as checking
            Assert.AreEqual(0.08m, Math.Round(interest, RoundingDigits));
        }

        [TestMethod]
        public void CalculateDailyInterestForSavingsAccountMonth()
        {
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.Savings);
            DateProvider.Instance.Now = () => DateTime.Now.AddDays(-30);
            account.Deposit(2000);
            DateProvider.Instance.Now = () => DateTime.Now;
            var interest = account.InterestEarned();
            Assert.AreEqual(0.25m, Math.Round(interest, RoundingDigits));
        }

        [TestMethod]
        public void CalculateDailyInterestForMaxiSavingsAccountYear()
        {
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.MaxiSavings);
            DateProvider.Instance.Now = () => DateTime.Now.AddYears(-1);
            account.Deposit(1000);
            DateProvider.Instance.Now = () => DateTime.Now;
            var interest = account.InterestEarned();
            Assert.AreEqual(51.23m, Math.Round(interest, RoundingDigits));
        }

        [TestMethod]
        public void CalculateDailyInterestForMaxiSavingsAccountComplexCase()
        {
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.MaxiSavings);
            DateProvider.Instance.Now = () => DateTime.Now.AddDays(-30);
            account.Deposit(2000);
            DateProvider.Instance.Now = () => DateTime.Now.AddDays(-20);
            account.Withdraw(500);
            DateProvider.Instance.Now = () => DateTime.Now.AddDays(-15);
            account.Deposit(500);
            DateProvider.Instance.Now = () => DateTime.Now;
            var interest = account.InterestEarned();
            Assert.AreEqual(5.26m, Math.Round(interest, RoundingDigits));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can deposit 0 dollars to checking account")]
        public void ThrowAnExceptionForDepositZeroToCheckingAccount()
        {
            IAccount checkingAccount = AccountFactory.Instance.Create(Account.AccountType.Checking);
            checkingAccount.Deposit(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can deposit -10 dollars to savings account")]
        public void ThrowAnExceptionForDepositLessThanZeroToSavingsAccount()
        {
            IAccount checkingAccount = AccountFactory.Instance.Create(Account.AccountType.Checking);
            checkingAccount.Deposit(-10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can withdrawal 0 dollars to maxi savings account")]
        public void ThrowAnExceptionForWithdrawalZeroFromMaxiSavingsAccount()
        {
            IAccount checkingAccount = AccountFactory.Instance.Create(Account.AccountType.Checking);
            checkingAccount.Withdraw(0);
        }
    }
}
