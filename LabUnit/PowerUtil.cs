namespace LabUnit
{
    public static class PowerUtil
    {
        public static double Power(double x, int n)
        {
            if (n == 0) return 1.0;
            if (n > 0) return x * Power(x, n - 1);
            return Power(x, n + 1) / x;
        }
    }
}