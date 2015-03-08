using System;

namespace abc_bank
{

    public sealed class DateProvider
    {
        private static DateProvider _instance;

        public static DateProvider Instance { get { return _instance ?? (_instance = new DateProvider()); } }

        public Func<DateTime> Now { get; set; }

        private DateProvider()
        {
            Now = () => DateTime.Now;
        }
    }
}
