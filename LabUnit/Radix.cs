using System;
using System.Collections.Generic;

namespace LabUnit
{
    public class Radix
    {
        private readonly int number;

        public Radix(int number)
        {
            if (number < 0) throw new ArgumentException("Incorrect Value");
            this.number = number;
        }

        public string ConvertDecimalToAnother(int radix = 2)
        {
            if (radix < 2 || radix > 16) throw new ArgumentException("Invalid Radix");
            if (number == 0) return "0";

            int n = number;
            var result = new List<char>();
            const string digits = "0123456789ABCDEF";

            while (n > 0)
            {
                result.Add(digits[n % radix]);
                n /= radix;
            }
            result.Reverse();
            return new string(result.ToArray());
        }
    }
}