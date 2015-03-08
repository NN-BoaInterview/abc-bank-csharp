using System;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

namespace abc_bank
{
    public class Bank
    {
        private readonly ICollection<Customer> _customers;

        public Bank()
        {
            _customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
        }

        public String CustomerSummary()
        {
            var sb = new StringBuilder();
            sb.Append("Customer Summary");
            foreach (var customer in _customers)
            {
                sb.Append(String.Format(CultureInfo.InvariantCulture, "\n - {0} ({1})", customer.Name, customer.GetNumberOfAccounts().FormatWord("account")));
            }
            return sb.ToString();
        }

        public Decimal TotalInterestPaid()
        {
            return _customers.Sum(c => c.TotalInterestEarned());
        }

        /// <summary>
        /// Not sure what this method is for, cannot see the business reason behind getting first customer
        /// </summary>
        /// <returns></returns>
        public String GetFirstCustomer()
        {
            try
            {
                return _customers.First().Name;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return "Error";
            }
        }
    }
}
