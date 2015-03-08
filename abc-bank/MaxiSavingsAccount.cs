using System;
using System.Linq;

namespace abc_bank
{
    public class MaxiSavingsAccount : Account
    {
        public MaxiSavingsAccount() : base(AccountType.MaxiSavings)
        {
        }

        protected override Double CompoundInterestForPeriod(Double principal, DateTime from, DateTime to)
        {
            if (!HasWithdrawalsWithinLast10Days(to))
            {
                var previousInterest = 0.0d;
                if ((to - from).TotalDays > 10 && HasWithdrawalsWithinLast10Days(from))
                {
                    // if we dont have withdrawal in the past 10 days and timeframe is more than 10 days need to do complex calculation
                    var transaction = Transactions.Where(t => t.Date < from).OrderByDescending(t => t.Date).First();
                    previousInterest = CompoundInterestForPeriod(principal, from, transaction.Date.AddDays(10));
                    from = transaction.Date.AddDays(10);
                }
                return previousInterest + (principal * Math.Pow(1 + (0.05 / DaysPerYear), Math.Floor((to - from).TotalDays))) - principal;
            }
            return (principal * Math.Pow(1 + (0.001 / DaysPerYear), Math.Floor((to - from).TotalDays))) - principal;
        }

        private Boolean HasWithdrawalsWithinLast10Days(DateTime date)
        {
            return Transactions.Where(t => t.Date < date).Any(t => t.Type == Transaction.TransactionType.Withdrawal && (date.Date - t.Date.Date).TotalDays <= 10);
        }
    }
}
