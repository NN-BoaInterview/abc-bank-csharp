using System;
using System.Collections.Generic;

namespace abc_bank
{
    public interface IAccount
    {
        Account.AccountType Type { get; }
        IList<Transaction> Transactions { get; }
        void Deposit(Double amount);
        void Withdraw(Double amount);
        Double SumTransactions();
        Double SumTransactions(DateTime toDate);
        Double InterestEarned();
    }
}
