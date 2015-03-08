using System;

namespace abc_bank
{
    public class SavingsAccount : Account
    {
        public SavingsAccount() : base(AccountType.Savings)
        {
        }

        protected override Double CompoundInterestForPeriod(Double principal, DateTime from, DateTime to)
        {
            if (principal <= 1000)
            {
                return (principal * Math.Pow(1 + (0.001 / DaysPerYear), Math.Floor((to - from).TotalDays))) - principal;
            }
            return CompoundInterestForPeriod(1000, from, to) + ((principal - 1000) * Math.Pow(1 + (0.002 / DaysPerYear), Math.Floor((to - from).TotalDays))) - (principal - 1000);
        }
    }
}
