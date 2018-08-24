using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms.Binary
{
    /// <summary>
    /// Logarithm calculator
    /// </summary>
    public class Logarithm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int CalculateBase2LogFloor(int x)
        {
            // make all right most bits after MSB top 1s
            // for example make ..001000 => 001111
            x = x | (x >> 1);
            x = x | (x >> 2);
            x = x | (x >> 4);
            x = x | (x >> 8);
            x = x | (x >> 16);

            // now log(x) base 2 = count the number of set bits - 1
            // to find the count to do the following steps:

            // set bit count of 2 bit groups
            // 0x0555.. will be like 010101010101..
            x = (x & 0x55555555) + ((x >> 1) & 0x55555555);

            // set bit count of 4 groups
            // 0x0333.. will be like 001100110011..
            x = (x & 0x33333333) + ((x >> 2) & 0x33333333);

            // set bit count of 8 bit groups of 4
            // 0x0f0f.. will be like 0000111100001111..
            x = (x & 0x0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F);

            // sum up four groups of 8 bit
            x = (x & 0x000000FF) +
                ((x >> 8) & 0x000000FF) +
                ((x >> 16) & 0x000000FF) +
                ((x >> 24) & 0x000000FF);

            // -1 for log of base 2
            return x - 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int CalculateBase10LogFloor(int x)
        {
            // using the below relation
            // log(x) base b = (log(x) base a) / (log(b) base a)
            var n = CalculateBase2LogFloor(x);
            var d = CalculateBase2LogFloor(10);
            return n / d;
        }
    }
}
