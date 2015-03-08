using System;

namespace abc_bank
{
    public class CheckingAccount : Account
    {
        public CheckingAccount() : base(AccountType.Checking)
        {
        }

        protected override Decimal CompoundInterestForPeriod(Decimal principal, DateTime from, DateTime to)
        {
            return (principal * (Decimal)Math.Pow(1 + (0.001 / DaysPerYear), Math.Floor((to - from).TotalDays))) - principal;
        }
    }
}
