using AdvancedAlgorithms.DataStructures.Heap.Min;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms.Compression
{
    /// <summary>
    /// A huffman coding implemtation using Fibornacci Min Heap
    /// </summary>
    public class HuffmanCoding<T>
    {
        /// <summary>
        /// Returns a ditionary of chosen encoding bytes for each distinct T
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public Dictionary<T, byte[]> Compress(T[] input)
        //{
        //    var frequencies = ComputeFrequency(input);

        //    //var minHeap = new BMinHeap<FrequencyWrap>();

        //    //foreach (var frequency in frequencies)
        //    //{
        //    //    minHeap.
        //    //}
        //}

        /// <summary>
        /// Now gether the codes
        /// </summary>
        private void Dfs(FrequencyWrap currentNode, List<byte> pathStack, Dictionary<T, byte[]> result)
        {
            if (currentNode.IsLeaf)
            {
                result.Add(currentNode.Item, pathStack.ToArray());
                return;
            }

            if (currentNode.Left != null)
            {
                pathStack.Add(0);
                Dfs(currentNode.Left, pathStack, result);
                pathStack.RemoveAt(pathStack.Count - 1);
            }

            if (currentNode.Right != null)
            {
                pathStack.Add(1);
                Dfs(currentNode.Right, pathStack, result);
                pathStack.RemoveAt(pathStack.Count - 1);
            }
        }

        /// <summary>
        /// Computes frequencies of each T in given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Dictionary<T, int> ComputeFrequency(T[] input)
        {
            var result = new Dictionary<T, int>();

            foreach (var item in input)
            {
                if (!result.ContainsKey(item))
                {
                    result.Add(item, 1);
                    continue;
                }
                result[item]++;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private class FrequencyWrap : IComparable
        {
            public T Item { get; }
            public int Frequency { get; }

            public FrequencyWrap Left { get; set; }
            public FrequencyWrap Right { get; set; }
            public bool IsLeaf => Left == null && Right == null;

            public FrequencyWrap(T item, int frequency)
            {
                Item = item;
                Frequency = frequency;
            }

            public int CompareTo(object obj)
            {
                return Frequency.CompareTo(((FrequencyWrap) obj).Frequency);
            }
        }
    }
}
