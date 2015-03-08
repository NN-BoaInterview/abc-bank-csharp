using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            IAccount checkingAccount = AccountFactory.Instance.Create(Account.AccountType.Checking);
            IAccount savingsAccount = AccountFactory.Instance.Create(Account.AccountType.Savings);
            Customer customer = new Customer("Henry")
                    .OpenAccount(checkingAccount)
                    .OpenAccount(savingsAccount);
            checkingAccount.Deposit(100);
            savingsAccount.Deposit(4000);
            savingsAccount.Withdraw(200);
            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", customer.GetStatement());
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer customer = new Customer("Oscar").OpenAccount(new SavingsAccount());
            Assert.AreEqual(1, customer.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer customer = new Customer("Oscar")
                    .OpenAccount(Account.AccountType.Savings)
                    .OpenAccount(Account.AccountType.Checking);
            Assert.AreEqual(2, customer.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer customer = new Customer("Oscar")
                    .OpenAccount(Account.AccountType.Savings)
                    .OpenAccount(Account.AccountType.Checking)
                    .OpenAccount(Account.AccountType.MaxiSavings);
            Assert.AreEqual(3, customer.GetNumberOfAccounts());
        }

        [TestMethod]
        public void CanTransferFundsBetweenAccounts()
        {
            IAccount checkingAccount = AccountFactory.Instance.Create(Account.AccountType.Checking);
            IAccount savingsAccount = AccountFactory.Instance.Create(Account.AccountType.Savings);
            Customer customer = new Customer("Oscar")
                    .OpenAccount(checkingAccount)
                    .OpenAccount(savingsAccount);
            savingsAccount.Deposit(1000);
            customer.TransferFunds(savingsAccount, checkingAccount, 500);
            Assert.AreEqual("Statement for Oscar\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $500.00\n" +
                    "Total $500.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $1,000.00\n" +
                    "  withdrawal $500.00\n" +
                    "Total $500.00\n" +
                    "\n" +
                    "Total In All Accounts $1,000.00", customer.GetStatement());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Customer was able to transfer fund from account not owned by him")]
        public void ThrowAnExceptionForTransferFundsFromAccountNotOwnedByCustomer()
        {
            IAccount checkingAccount = AccountFactory.Instance.Create(Account.AccountType.Checking);
            IAccount savingsAccount = AccountFactory.Instance.Create(Account.AccountType.Savings);
            Customer customer = new Customer("Oscar").OpenAccount(checkingAccount);
            savingsAccount.Deposit(1000);
            customer.TransferFunds(savingsAccount, checkingAccount, 500);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Customer was able to transfer fund to account not owned by him")]
        public void ThrowAnExceptionForTransferFundsToAccountNotOwnedByCustomer()
        {
            IAccount checkingAccount = AccountFactory.Instance.Create(Account.AccountType.Checking);
            IAccount savingsAccount = AccountFactory.Instance.Create(Account.AccountType.Savings);
            Customer customer = new Customer("Oscar").OpenAccount(savingsAccount);
            savingsAccount.Deposit(1000);
            customer.TransferFunds(savingsAccount, checkingAccount, 500);
        }
    }
}
