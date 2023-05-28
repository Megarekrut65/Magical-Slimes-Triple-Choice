
using System;

namespace Main
{
    public static class Version
    {
        public const string Current = "2.4";

        public static double ToNumber(string value)
        {
            string[] parts = value.Split(".");

            double res = 0;
            double ten = Math.Pow(10, parts.Length-1);
            foreach (string part in parts)
            {
                res += ten * Convert.ToDouble(part);
                ten = Math.Round(ten/10);
            }

            return res;
        }
    }
}