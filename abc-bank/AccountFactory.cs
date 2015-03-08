using System;

namespace abc_bank
{
    public sealed class AccountFactory
    {
        private static AccountFactory _instance;

        public static AccountFactory Instance { get { return _instance ?? (_instance = new AccountFactory()); } }

        private AccountFactory()
        {
        }

        public IAccount Create(Account.AccountType accountType)
        {
            switch (accountType)
            {
                case Account.AccountType.MaxiSavings:
                    return new MaxiSavingsAccount();
                case Account.AccountType.Savings:
                    return new SavingsAccount();
                case Account.AccountType.Checking:
                    return new CheckingAccount();
                default:
                    throw new IndexOutOfRangeException("Unknown account type.");
            }
        }
    }
}
