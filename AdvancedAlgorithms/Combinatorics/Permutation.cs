using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms.Combinatorics
{
    /// <summary>
    /// Permutation computer
    /// </summary>
    public class Permutation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="r"></param>
        /// <param name="withRepitition"></param>
        /// <returns></returns>
        public static List<List<T>> Find<T>(List<T> input, int r, bool withRepitition = false)
        {
            var result = new List<List<T>>();

            Recurse(input, r, withRepitition, new List<T>(), new HashSet<int>(), result);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="r"></param>
        /// <param name="withRepitition"></param>
        /// <param name="prefix"></param>
        /// <param name="prefixIndices"></param>
        /// <param name="result"></param>
        private static void Recurse<T>(List<T> input, int r, bool withRepitition, List<T> prefix,
            HashSet<int> prefixIndices, List<List<T>> result)
        {
            if (prefix.Count == r)
            {
                result.Add(new List<T>(prefix));
                return;
            }

            for (int j = 0; j < input.Count; j++)
            {
                if (prefixIndices.Contains(j) && !withRepitition)
                {
                    continue;
                }

                prefix.Add(input[j]);
                prefixIndices.Add(j);

                Recurse(input, r, withRepitition, prefix, prefixIndices, result);

                prefix.RemoveAt(prefix.Count - 1);
                prefixIndices.Remove(j);
            }
        }
    }
}
