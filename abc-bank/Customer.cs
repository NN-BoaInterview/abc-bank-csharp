using System;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

namespace abc_bank
{
    public class Customer
    {
        public String Name { get; private set; }
        private ICollection<IAccount> Accounts { get; set; }

        public Customer(String name)
        {
            Name = name;
            Accounts = new List<IAccount>();
        }

        public Customer OpenAccount(IAccount account)
        {
            Accounts.Add(account);
            return this;
        }

        public Customer OpenAccount(Account.AccountType accountType)
        {
            return OpenAccount(AccountFactory.Instance.Create(accountType));
        }

        public Int32 GetNumberOfAccounts()
        {
            return Accounts.Count;
        }

        public Double TotalInterestEarned()
        {
            return Accounts.Sum(a => a.InterestEarned());
        }

        public void TransferFunds(IAccount from, IAccount to, Double amount)
        {
            if (!Accounts.Contains(from))
            {
                throw new ArgumentException("From account not found for customer");
            }
            if (!Accounts.Contains(to))
            {
                throw new ArgumentException("From account not found for customer");
            }
            from.Withdraw(amount);
            to.Deposit(amount);
        }

        public String GetStatement()
        {
            var sb = new StringBuilder();
            sb.Append("Statement for " + Name + "\n");
            var total = 0.0d;
            foreach (var account in Accounts)
            {
                sb.Append(String.Format(CultureInfo.InvariantCulture, "\n{0}\n", StatementForAccount(account)));
                total += account.SumTransactions();
            }
            sb.Append(String.Format(CultureInfo.InvariantCulture, "\nTotal In All Accounts {0}", total.ToCurrency()));
            return sb.ToString();
        }

        private static String StatementForAccount(IAccount account)
        {
            var sb = new StringBuilder();
            sb.Append(account.Type.GetDescription() + "\n");
            var total = 0.0d;
            foreach (var transaction in account.Transactions)
            {
                sb.Append(String.Format(CultureInfo.InvariantCulture, "  {0}\n", transaction));
                total += transaction.Amount;
            }
            sb.Append(String.Format(CultureInfo.InvariantCulture, "Total {0}", total.ToCurrency()));
            return sb.ToString();
        }
    }
}
