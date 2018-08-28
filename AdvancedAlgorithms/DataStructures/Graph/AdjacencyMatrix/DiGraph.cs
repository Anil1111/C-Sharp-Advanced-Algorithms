using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms.DataStructures.Graph.AdjacencyMatrix
{
    /// <summary>
    /// A directed graph implementation using dynamically growing / shrinking adjacency matrix array.
    /// IEnumerable enumerates all verticles.
    /// </summary>
    public class DiGraph<T> : IEnumerable<T>
    {
        public int VerticlesCount => usedSize;

        private Dictionary<T, int> vertexIndicies;
        private Dictionary<int, T> reverseVertexIndicies;

        private BitArray[] matrix;

        private int maxSize;
        private int usedSize;
        private int nextAvailableIndex;

        public DiGraph()
        {
            maxSize = 1;
            vertexIndicies = new Dictionary<T, int>();
            reverseVertexIndicies = new Dictionary<int, T>();
            matrix = new BitArray[maxSize];

            for (var i = 0; i < maxSize; i++)
            {
                matrix[i] = new BitArray(maxSize);
            }
        }

        /// <summary>
        /// Add a new vertex to this graph.
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="value"></param>
        public void AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!vertexIndicies.ContainsKey(value))
            {
                throw new Exception("Vertex exists.");
            }

            if (usedSize < maxSize / 2)
            {
                HalfMatrixSize();
            }

            if (nextAvailableIndex == maxSize)
            {
                DoubleMatrixSize();
            }
            vertexIndicies.Add(value, nextAvailableIndex);
            reverseVertexIndicies.Add(nextAvailableIndex, value);

            nextAvailableIndex++;
            usedSize++;
        }

        /// <summary>
        /// Remove an existing vertex from the graph.
        /// Time complexity: O(V) where V is the number of vertices.
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void RemoveVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!vertexIndicies.ContainsKey(value))
            {
                throw new Exception("Vertex does not exist.");
            }

            if (usedSize <= maxSize / 2)
            {
                HalfMatrixSize();
            }

            var index = vertexIndicies[value];

            // Clear edges
            for (var i = 0; i < maxSize; i++)
            {
                matrix[i].Set(index, false);
                matrix[index].Set(i, false);
            }
            reverseVertexIndicies.Remove(index);
            vertexIndicies.Remove(value);

            // decrement usedSize variable.
            usedSize--;
        }

        /// <summary>
        /// Add a edge fro m source to destination vertex.
        /// Time complexity: O(1).
        /// </summary>
        public void AddEdge(T source, T destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentException();
            }

            if (!vertexIndicies.ContainsKey(source) || !vertexIndicies.ContainsKey(destination))
            {
                throw new Exception("Source or Destination vertex does not exist.");
            }

            var sourceIndex = vertexIndicies[source];
            var destinationIndex = vertexIndicies[destination];

            if (matrix[sourceIndex].Get(destinationIndex))
            {
                throw new Exception("Edge already exists.");
            }
            matrix[sourceIndex].Set(destinationIndex, true);

        }

        /// <summary>
        /// remove an existing edge between source and destination.
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void RemoveEdge(T source, T destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentException();
            }

            if (!vertexIndicies.ContainsKey(source) || !vertexIndicies.ContainsKey(destination))
            {
                throw new Exception("Source or Destination vertex do not exist.");
            }

            var sourceIndex = vertexIndicies[source];
            var destinationIndex = vertexIndicies[destination];

            if (!matrix[sourceIndex].Get(destinationIndex))
            {
                throw new Exception("Edge does not exist.");
            }
            matrix[sourceIndex].Set(destinationIndex, false);
        }

        /// <summary>
        /// Do we have an edge between the given source and destination.
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool HasEdge(T source, T destination)
        {
            if (source == null | destination == null)
            {
                throw new ArgumentException();
            }

            if (!vertexIndicies.ContainsKey(source) || !vertexIndicies.ContainsKey(destination))
            {
                throw new Exception("Source or Destination does not exist.");
            }

            var sourceIndex = vertexIndicies[source];
            var destinitionIndex = vertexIndicies[destination];

            return matrix[sourceIndex].Get(destinitionIndex);
        }

        public IEnumerable<T> OutEdges(T vertex)
        {
            if (!vertexIndicies.ContainsKey(vertex))
            {
                throw new ArgumentException("Vertex is not in this graph.");
            }

            var index = vertexIndicies[vertex];

            var result = new List<T>();

            for (var i = 0; i < maxSize; i++)
            {
                if (matrix[index].Get(i))
                {
                    result.Add(reverseVertexIndicies[i]);
                }
            }
            return result;
        }

        public IEnumerable InEdges(T vertex)
        {
            if (!vertexIndicies.ContainsKey(vertex))
            {
                throw new ArgumentException("Vertex is not in this graph.");
            }

            var index = vertexIndicies[vertex];

            var result = new List<T>();

            for (var i = 0; i < maxSize; i++)
            {
                if (!matrix[index].Get(i))
                {
                    result.Add(reverseVertexIndicies[i]);
                }
            }
            return result;
        }

        private void DoubleMatrixSize()
        {
            var newMatrix = new BitArray[maxSize * 2];

            for (var i = 0; i < maxSize * 2; i++)
            {
                newMatrix[i] = new BitArray(maxSize * 2);
            }

            var newVertexIndicies = new Dictionary<T, int>();
            var newReverseIndices = new Dictionary<int, T>();

            var k = 0;

            foreach (var vertex in vertexIndicies)
            {
                newVertexIndicies.Add(vertex.Key, k);
                newReverseIndices.Add(k, vertex.Key);
                k++;
            }
            nextAvailableIndex = k;

            for (var i = 0; i < maxSize; i++)
            {
                newMatrix[i] = new BitArray(maxSize * 2);

                for (var j = 0; j < maxSize; j++)
                {
                    if (!matrix[i].Get(j) || !reverseVertexIndicies.ContainsKey(i)
                        || !reverseVertexIndicies.ContainsKey(j))
                    {
                        continue;
                    }
                    var newI = newVertexIndicies[reverseVertexIndicies[i]];
                    var newJ = newVertexIndicies[reverseVertexIndicies[j]];

                    newMatrix[newI].Set(newJ, true);
                }
            }

            matrix = newMatrix;
            vertexIndicies = newVertexIndicies;
            reverseVertexIndicies = newReverseIndices;
            maxSize *= 2;
        }

        private void HalfMatrixSize()
        {
            var newMatrix = new BitArray[maxSize / 2];

            for (var i = 0; i < maxSize / 2; i++)
            {
                newMatrix[i] = new BitArray(maxSize / 2);
            }

            var newVertexIndicies = new Dictionary<T, int>();
            var newReverseIndicies = new Dictionary<int, T>();

            var k = 0;

            foreach (var vertex in vertexIndicies)
            {
                newVertexIndicies.Add(vertex.Key, k);
                newReverseIndicies.Add(k, vertex.Key);
                k++;
            }
            nextAvailableIndex = k;

            for (var i = 0; i < maxSize; i++)
            {
                for (var j = 0; j < maxSize; j++)
                {
                    if (!matrix[i].Get(j) || !reverseVertexIndicies.ContainsKey(i) 
                        || !reverseVertexIndicies.ContainsKey(j))
                    {
                        continue;
                    }
                    var newI = newVertexIndicies[reverseVertexIndicies[i]];
                    var newJ = newVertexIndicies[reverseVertexIndicies[j]];

                    newMatrix[newI].Set(newJ, true);
                }
            }
            matrix = newMatrix;
            vertexIndicies = newVertexIndicies;
            reverseVertexIndicies = newReverseIndicies;
            maxSize /= 2;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return vertexIndicies.Select(x => x.Key).GetEnumerator();
        }
    }
}
