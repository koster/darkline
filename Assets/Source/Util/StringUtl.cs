using UnityEngine;

namespace Source.Util
{
    public static class StringUtl
    {
        public static string Signed(int i)
        {
            if (i > 0)
                return "+" + i;
            if (i == 0)
                return "0";
            return i.ToString();
        }

        public static string AsPercent(this float s)
        {
            return Mathf.Round(s * 100) + "%";
        }
    }
}