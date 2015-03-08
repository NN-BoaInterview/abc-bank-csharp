using System;

namespace abc_bank
{
    public class CheckingAccount : Account
    {
        public CheckingAccount() : base(AccountType.Checking)
        {
        }

        protected override Double CompoundInterestForPeriod(Double principal, DateTime from, DateTime to)
        {
            return (principal * Math.Pow(1 + (0.001 / DaysPerYear), Math.Floor((to - from).TotalDays))) - principal;
        }
    }
}
