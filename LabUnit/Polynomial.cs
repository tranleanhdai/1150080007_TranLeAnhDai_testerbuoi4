using System;
using System.Collections.Generic;

namespace LabUnit
{
    public class Polynomial
    {
        private readonly int n;
        private readonly List<int> a;

        public Polynomial(int n, List<int> a)
        {
            if (n < 0 || a == null || a.Count != n + 1)
                throw new ArgumentException("Invalid Data");

            this.n = n;
            this.a = a;
        }

        public int Cal(int x)
        {
            long result = 0;
            long pow = 1;
            for (int i = 0; i <= n; i++)
            {
                result += a[i] * pow;
                pow *= x;
            }
            return (int)result;
        }
    }
}