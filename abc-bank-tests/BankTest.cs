using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        // Round interest rate to the nearest 2 digits (cents)
        private const Int32 RoundingDigits = 2;
        private const Double DoubleDelta = 1e-15;

        [TestMethod]
        public void CustomerSummary()
        {
            Bank bank = new Bank();
            Customer customer = new Customer("John");
            customer.OpenAccount(AccountFactory.Instance.Create(Account.AccountType.Checking));
            bank.AddCustomer(customer);
            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount()
        {
            Bank bank = new Bank();
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.Checking);
            Customer customer = new Customer("Bill").OpenAccount(account);
            bank.AddCustomer(customer);
            DateProvider.Instance.Now = () => DateTime.Now.AddYears(-1);
            account.Deposit(100);
            DateProvider.Instance.Now = () => DateTime.Now;
            Assert.AreEqual(0.1, Math.Round(bank.TotalInterestPaid(), RoundingDigits), DoubleDelta);
        }

        [TestMethod]
        public void SavingsAccountLessThanThousandDeposit()
        {
            Bank bank = new Bank();
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.Savings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(account));
            DateProvider.Instance.Now = () => DateTime.Now.AddYears(-1);
            account.Deposit(500);
            DateProvider.Instance.Now = () => DateTime.Now;
            Assert.AreEqual(0.5, Math.Round(bank.TotalInterestPaid(), RoundingDigits), DoubleDelta);
        }

        [TestMethod]
        public void SavingsAccountOverThousandDeposit()
        {
            Bank bank = new Bank();
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.Savings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(account));
            DateProvider.Instance.Now = () => DateTime.Now.AddYears(-1);
            account.Deposit(1500);
            DateProvider.Instance.Now = () => DateTime.Now;
            Assert.AreEqual(2.0, Math.Round(bank.TotalInterestPaid(), RoundingDigits), DoubleDelta);
        }

        [TestMethod]
        public void MaxiSavingsAccount()
        {
            Bank bank = new Bank();
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.MaxiSavings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(account));
            DateProvider.Instance.Now = () => DateTime.Now.AddYears(-1);
            account.Deposit(3000);
            DateProvider.Instance.Now = () => DateTime.Now;
            Assert.AreEqual(153.69, Math.Round(bank.TotalInterestPaid(), RoundingDigits), DoubleDelta);
        }

        [TestMethod]
        public void MaxiSavingsAccountWithdrawal()
        {
            Bank bank = new Bank();
            IAccount account = AccountFactory.Instance.Create(Account.AccountType.MaxiSavings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(account));
            DateProvider.Instance.Now = () => DateTime.Now.AddYears(-1);
            account.Deposit(3000);
            DateProvider.Instance.Now = () => DateTime.Now.AddDays(-182);
            account.Withdraw(1000);
            DateProvider.Instance.Now = () => DateTime.Now;
            Assert.AreEqual(128.47, Math.Round(bank.TotalInterestPaid(), RoundingDigits), DoubleDelta);
        }

        [TestMethod]
        public void CanGetFirstCustomer()
        {
            Bank bank = new Bank();
            bank.AddCustomer(new Customer("Bill"));
            bank.AddCustomer(new Customer("Jack"));

            Assert.AreEqual("Bill", bank.GetFirstCustomer());
        }

        [TestMethod]
        public void CanGetFirstCustomerWhenNoCustomers()
        {
            Bank bank = new Bank();
            Assert.AreEqual("Error", bank.GetFirstCustomer());
        }
    }
}
