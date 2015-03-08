using System;
using System.ComponentModel;

namespace abc_bank
{
    public class Transaction
    {
        public enum TransactionType
        {
            [Description("Deposit")]
            Deposit,

            [Description("Withdrawal")]
            Withdrawal,
        }

        private readonly Double _amount;

        private readonly DateTime _date;

        public TransactionType Type { get { return _amount > 0 ? TransactionType.Deposit : TransactionType.Withdrawal; } }
        public Double Amount { get { return _amount; } }
        public DateTime Date { get { return _date; } }

        public Transaction(Double amount, DateProvider dateProvider = null)
        {
            _amount = amount;
            _date = dateProvider != null ? dateProvider.Now() : DateProvider.Instance.Now();
        }

        public override String ToString()
        {
            return String.Format("{0} {1}", Type.GetDescription().ToLowerInvariant(), Amount.ToCurrency());
        }
    }
}
