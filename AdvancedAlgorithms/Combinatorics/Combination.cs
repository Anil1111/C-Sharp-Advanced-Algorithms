using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms.Combinatorics
{
    /// <summary>
    /// Combinations computer
    /// </summary>
    public class Combination
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="r"></param>
        /// <param name="withRepetition"></param>
        /// <returns></returns>
        public static List<List<T>> Find<T>(List<T> input , int r, bool withRepetition)
        {
            var result = new List<List<T>>();

            Recurse(input, r, withRepetition, 0 , new List<T>(), new HashSet<int>(), result);

            return result;   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="r"></param>
        /// <param name="withRepetition"></param>
        /// <param name="k"></param>
        /// <param name="prefix"></param>
        /// <param name="prefixIndices"></param>
        /// <param name="result"></param>
        private static void Recurse<T>(List<T> input, int r, bool withRepetition, int k,
             List<T> prefix, HashSet<int> prefixIndices, List<List<T>> result)
        {
            if (prefix.Count == r)
            {
                result.Add(new List<T>(prefix));
                return;
            }

            for (int j = k; j < input.Count; j++)
            {
                if (prefixIndices.Contains(j) && !withRepetition)
                {
                    continue;
                }

                prefix.Add(input[j]);
                prefixIndices.Add(j);

                Recurse(input, r, withRepetition, j, prefix, prefixIndices, result);

                prefix.RemoveAt(prefix.Count - 1);
                prefixIndices.Remove(j);
            }
        }
    }
}
