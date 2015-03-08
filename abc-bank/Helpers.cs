using System;
using System.ComponentModel;

namespace abc_bank
{
    public static class Helpers
    {
        public static String GetDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }

        public static String ToCurrency(this Double d)
        {
            return String.Format("{0:C}", Math.Abs(d));
        }

        public static String ToCurrency(this Decimal d)
        {
            return String.Format("{0:C}", Math.Abs(d));
        }

        /// <summary>
        /// Make sure correct plural of word is created based on the number passed in:
        /// If number passed in is 1 just return the word otherwise add an 's' at the end
        /// </summary>
        /// <param name="number"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static String FormatWord(this Int32 number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }
    }
}
