using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms.Binary
{
    /// <summary>
    /// GCD without division or mod operators but using subtraction.
    /// </summary>
    public class GCD
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Find(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }

            if (a == 0)
            {
                return b;
            }

            // Fix negetive numbers
            if (a < 0)
            {
                a = -a;
            }

            if (b < 0)
            {
                b = -b;
            }

            // p and q even
            if ((a & 1) == 0 & (b & 1) == 0)
            {
                // divide both a and b by 2
                // multiply by 2 the result
                return Find(a >> 1, b >> 1) << 1;
            }

            // a is even, b is odd
            if ((a & 1) == 0)
            {
                // divide by 2
                return Find(a >> 1, b);
            }

            // a is odd, b is even
            if ((b & 1) == 0)
            {
                return Find(a, b >> 1);
            }

            // a and b odd, a >= b
            if (a >= b)
            {
                // since subtracting two odd numbers gives a even number
                // divide ( a - b ) by 2 to reduce calculations
                return Find((a - b) >> 1, b);
            }

            // a and b odd, a < b

            // since subtracting two odd numbers gives a even number
            // divide ( b - a ) by 2 to resuce calculations
            return Find(a, (b - a) >> 1);
        }


    }
}
