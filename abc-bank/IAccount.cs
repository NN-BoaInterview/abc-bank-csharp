using System;
using System.Collections.Generic;

namespace abc_bank
{
    public interface IAccount
    {
        Account.AccountType Type { get; }
        IList<Transaction> Transactions { get; }
        void Deposit(Decimal amount);
        void Withdraw(Decimal amount);
        Decimal SumTransactions();
        Decimal SumTransactions(DateTime toDate);
        Decimal InterestEarned();
    }
}
