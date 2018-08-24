using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms.Binary
{
    /// <summary>
    /// Base summary implementation
    /// </summary>
    public class BaseConversation
    {
        /// <summary>
        /// Converts base of given to the target base.
        /// </summary>
        /// <param name="srcNumber">Input number in scource base system</param>
        /// <param name="srcBaseChars">Source base system characters in ascending order</param>
        /// <param name="dstbaseChars">Destination base system characters in incresing order</param>
        /// <param name="precision">Required precision when dealing with factions.  Defaults to 32 places.</param>
        /// <returns>The result in target base as a string</returns>
        public static string Convert(string srcNumber, string srcBaseChars,
            string dstBaseChars, int precision = 12)
        {
            srcNumber = srcNumber.Trim();
            if (srcNumber.Contains("."))
            {
                var tmp = srcNumber.Split('.');
                var whole = tmp[0].TrimEnd();
                var fraction = tmp[1].TrimStart();

                return ConvertWhole(whole, srcBaseChars, dstBaseChars) + 
                    "." + ConvertFraction(fraction, srcBaseChars, dstBaseChars, precision);
            }
            return ConvertWhole(srcNumber, srcBaseChars, dstBaseChars);
        }

        /// <summary>
        /// Converts the whole part of source number.
        /// </summary>
        /// <param name="srcNumber"></param>
        /// <param name="srcbaseChars"></param>
        /// <param name="dstBaseChars"></param>
        /// <returns></returns>
        private static string ConvertWhole(string srcNumber, string srcBaseChars,
            string dstBaseChars)
        {
            if (string.IsNullOrEmpty(srcNumber))
            {
                return string.Empty;
            }

            var srcBase = srcBaseChars.Length;
            var dstBase = dstBaseChars.Length;

            if (srcBase <= 1)
            {
                throw new Exception("Invalid source base length.");
            }

            if (dstBase <= 1)
            {
                throw new Exception("Invalid destination base length.");
            }

            long base10Result = 0;
            var j = 0;
            // Convert to base 10
            // Move from least to most significant numbers
            for (int i = srcNumber.Length - 1; i >= 0; i--)
            {
                // eg. 1 * 2^0
                base10Result += (srcBaseChars.IndexOf(srcNumber[i])) *
                    (long)(Math.Pow(srcBase, j));
                j++;
            }

            var result = new StringBuilder();
            // Now convert to target base.
            while (base10Result != 0)
            {
                var rem = (int)base10Result % dstBase;
                result.Insert(0, dstBaseChars[rem]);
                base10Result = base10Result / dstBase;
            }
            return result.ToString();
        }

        

        /// <summary>
        /// Converts the fractional part of source number.
        /// </summary>
        /// <param name="srcNumber"></param>
        /// <param name="srcBaseChars"></param>
        /// <param name="dstBaseChars"></param>
        /// <param name="maxPrecision"></param>
        /// <returns></returns>
        private static string ConvertFraction(string srcNumber, string srcBaseChars,
            string dstBaseChars, int maxPrecision)
        {
            if (string.IsNullOrEmpty(srcNumber))
            {
                return string.Empty;
            }

            var srcBase = srcBaseChars.Length;
            var dstBase = dstBaseChars.Length;

            if (srcBase <= 1)
            {
                throw new Exception("Invalid source base length.");
            }

            if (dstBase <= 1)
            {
                throw new Exception("Invalid destination base length");
            }

            decimal base10Result = 0;
            // Convert to base 10
            // Move from most significant numbers to least
            for (int i = 0; i < srcNumber.Length; i++)
            {
                // eg. 1 8 1 / (2^1)
                base10Result = += (srcBaseChars.IndexOf(srcNumber[i])) *
                    (decimal)(1 / Math.Pow(srcBase, i + 1));
            }

            var result = new StringBuilder();
            // Now convert to target base
            while (base10Result != 0 && maxPrecision > 0)
            {
                base10Result = base10Result * dstBase;
                result.Append(dstBaseChars[(int)Math.Floor(base10Result)]);
                base10Result -= Math.Floor(base10Result);
                maxPrecision--;
            }

            return result.ToString();

        }
    }
}
