using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

namespace abc_bank
{
    public abstract class Account : IAccount
    {
        public enum AccountType
        {
            [Description("Checking Account")]
            Checking,

            [Description("Savings Account")]
            Savings,

            [Description("Maxi Savings Account")]
            MaxiSavings
        }

        /// <summary>
        /// We have 365.25 days per year
        /// </summary>
        protected const Double DaysPerYear = 365.25;
        private readonly AccountType _accountType;
        protected readonly DateProvider DateProvider;

        public IList<Transaction> Transactions { get; private set; }

        public AccountType Type { get { return _accountType; } }

        protected Account(AccountType accountType, DateProvider dateProvider = null)
        {
            _accountType = accountType;
            DateProvider = dateProvider ?? DateProvider.Instance;
            Transactions = new List<Transaction>();
        }

        public void Deposit(Decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            Transactions.Add(new Transaction(amount, DateProvider));
        }

        public void Withdraw(Decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            Transactions.Add(new Transaction(-amount, DateProvider));
        }

        public Decimal SumTransactions()
        {
            return SumTransactions(DateProvider.Now());
        }

        public Decimal SumTransactions(DateTime toDate)
        {
            return Transactions.Where(t => t.Date <= toDate).Sum(t => t.Amount);
        }

        protected abstract Decimal CompoundInterestForPeriod(Decimal principal, DateTime from, DateTime to);

        public Decimal InterestEarned()
        {
            // We calculate daily rate according to the following formula:
            // A = P * (1 + r / m) ^ n :: A = amount earned, P = principal, r = annual interest rate, m = number of periods, n = total periods
            var transactions = Transactions.AsEnumerable().GroupBy(t => t.Date.Date).OrderBy(t => t.Key).ToDictionary(d => d.Key, a => a.Sum(t => t.Amount));
            var totalInterest = 0.0m;
            var previousTransaction = transactions.First();
            var principal = previousTransaction.Value;
            var interest = 0.0m;
            foreach (var transaction in transactions.Skip(1))
            {
                interest = CompoundInterestForPeriod(principal, previousTransaction.Key, transaction.Key);
                totalInterest += interest;
                principal += transaction.Value + interest;
                previousTransaction = transaction;
            }
            if (DateProvider.Now().Date > transactions.Last().Key)
            {
                var last = transactions.Last();
                interest = CompoundInterestForPeriod(principal, last.Key, DateProvider.Now().Date);
                totalInterest += interest;
                principal += interest;
            }
            return totalInterest;
        }
    }
}
