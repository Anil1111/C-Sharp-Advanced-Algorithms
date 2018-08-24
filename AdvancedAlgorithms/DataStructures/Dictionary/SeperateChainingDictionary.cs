using AdvancedAlgorithms.DataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdvancedAlgorithms.DataStructures.Dictionary.Dictionary<K, V>;

namespace AdvancedAlgorithms.DataStructures.Dictionary
{
    /// <summary>
    /// 
    /// </summary>
    internal class SeperateChainingDictionary<K, V> : IDictionary<K, V>
    {
        private const double tolerence = 0.1;

        private DoublyLinkedList<DictionaryNode<K, V>>[] hashArray;
        private int bucketSize => hashArray.Length;
        private readonly int initialBucketSize;
        private int filledBuckets;

        public int Count { get; private set; }

        public SeperateChainingDictionary(int initialBucketSize = 3)
        {
            this.initialBucketSize = initialBucketSize;
            hashArray = new DoublyLinkedList<DictionaryNode<K, V>>[initialBucketSize];
        }

        public V this[K key]
        {
            
        }

        public bool ContainsKey(K key)
        {

        }

        public void Add(K key, V value)
        {

        }

        public void Remove(K key)
        {

        }


    }
}
