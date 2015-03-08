using System;

namespace abc_bank
{
    public class SavingsAccount : Account
    {
        public SavingsAccount() : base(AccountType.Savings)
        {
        }

        protected override Decimal CompoundInterestForPeriod(Decimal principal, DateTime from, DateTime to)
        {
            if (principal <= 1000)
            {
                return (principal * (Decimal)Math.Pow(1 + (0.001 / DaysPerYear), Math.Floor((to - from).TotalDays))) - principal;
            }
            return CompoundInterestForPeriod(1000, from, to) + ((principal - 1000) * (Decimal)Math.Pow(1 + (0.002 / DaysPerYear), Math.Floor((to - from).TotalDays))) - (principal - 1000);
        }
    }
}
