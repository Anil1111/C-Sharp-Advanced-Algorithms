using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms.DataStructures.Graph.AdjacencyMatrix
{
    /// <summary>
    /// A weighted graph implementation using dynamically growing / shrinking adjacency matrix array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TW"></typeparam>
    public class WeightedDiGraph<T, TW> : IEnumerable<T> where TW : IComparable
    {
        public int VerticlesCount => usedSize;

        private Dictionary<T, int> vertexIndicies;
        private Dictionary<int, T> reverseVertexIndicies;

        private TW[,] matrix;

        private int maxSize;
        private int usedSize;
        private int nextAvailibleIndex;

        public WeightedDiGraph()
        {
            maxSize= 1;
            vertexIndicies = new Dictionary<T, int>();
            reverseVertexIndicies = new Dictionary<int, T>();
            matrix = new TW[maxSize, maxSize];
        }

        /// <summary>
        /// Add a new vertex to this graph.
        /// Time complexity: O(1).
        /// </summary>
        /// <param name="value"></param>
        public void AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (vertexIndicies.ContainsKey(value))
            {
                throw new Exception("Vertex exists.");
            }

            if (usedSize < maxSize / 2)
            {
                
            }

            if (nextAvailibleIndex == maxSize)
            {

            }

            vertexIndicies.Add(value, nextAvailibleIndex);
            reverseVertexIndicies.Add(nextAvailibleIndex, value);
            nextAvailibleIndex++;
            usedSize++;
        }

        /// <summary>
        /// Remove the given vertex.
        /// Time complexity: O(V) where V is the number of verticles.
        /// </summary>
        /// <param name="value"></param>
        public void RemoveVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (vertexIndicies.ContainsKey(value))
            {
                throw new Exception("Vertex doesn't exist.");
            }

            if (usedSize < maxSize / 2)
            {

            }

            var index = vertexIndicies[value];

            // clear edges
            for (int i = 0; i < maxSize; i++)
            {
                matrix[i, index] = default(TW);
                matrix[index, i] = default(TW);
            }

            reverseVertexIndicies.Remove(index);
            vertexIndicies.Remove(value);

            usedSize--;
        }

        /// <summary>
        /// Add a new edge to this graph.
        /// Time complexity: O(1).
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="weight"></param>
        public void AddEdge(T source, T destination, TW weight)
        {
            if (weight.Equals(default(TW)))
            {
                throw new Exception("Cannot add default edge weight.");
            }

            if (source == null || destination == null)
            {
                throw new ArgumentException();
            }

            if (!vertexIndicies.ContainsKey(source) || !vertexIndicies.ContainsKey(destination))
            {
                throw new Exception("Source or Destination vertex doesn't exist.");
            }

            var sourceIndex = vertexIndicies[source];
            var destinationIndex = vertexIndicies[destination];

            if (!matrix[sourceIndex, destinationIndex].Equals(default(TW)))
            {
                throw new Exception("Edge already exists.");
            }

            matrix[sourceIndex, destinationIndex] = weight;
        }

        /// <summary>
        /// Remove the given edge from this graph.
        /// Time complexity: O(1).
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
                throw new Exception("Source or Destination vertex doesn't exist.");
            }

            var sourceIndex = vertexIndicies[source];
            var destinationIndex = vertexIndicies[destination];

            if (matrix[sourceIndex, destinationIndex].Equals(default(TW)))
            {
                throw new Exception("Edge does not exist.");
            }

            matrix[sourceIndex, destinationIndex] = default(TW);
        }

        /// <summary>
        /// Do we have an edge betwwen given source and destination.
        /// Time complexity: O(1).
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool HashEdge(T source, T destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentException();
            }

            if (!vertexIndicies.ContainsKey(source) || !vertexIndicies.ContainsKey(destination))
            {
                throw new Exception("Source or destination vertex doesn't exist.");
            }

            var sourceIndex = vertexIndicies[source];
            var destinationIndex = vertexIndicies[destination];

            if (!matrix[sourceIndex, destinationIndex].Equals(default(TW)))
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Tuple<T, TW>> OutEdges(T vertex)
        {
            if (!vertexIndicies.ContainsKey(vertex))
            {
                throw new ArgumentException("Vertex is not in this graph.");
            }

            var index = vertexIndicies[vertex];

            var result = new List<Tuple<T, TW>>();

            for (int i = 0; i < maxSize; i++)
            {
                if (!matrix[index, i].Equals(default(TW)))
                {
                    result.Add(new Tuple<T, TW>(reverseVertexIndicies[i], matrix[index,i]));
                }
            }
            return result;
        }

        public IEnumerable<Tuple<T, TW>> InEdges(T vertex)
        {
            if (!vertexIndicies.ContainsKey(vertex))
            {
                throw new ArgumentException("Vertex is not in this graph.");
            }

            var index = vertexIndicies[vertex];

            var result = new List<Tuple<T, TW>>();

            for (int i = 0; i < maxSize; i++)
            {
                if(!matrix[i, index].Equals(default(TW)))
                {
                    result.Add(new Tuple<T, TW>(reverseVertexIndicies[i], matrix[index, i]));
                }
            }

            return result;
        }

        private void DoubleMatrixSize()
        {
            var newMatrix = new TW[maxSize * 2, maxSize * 2];

            var newVertexIndicies = new Dictionary<T, int>();
            var newReverseVertexIndicies = new Dictionary<int, T>();

            int k = 0;

            foreach (var vertex in vertexIndicies)
            {
                newVertexIndicies.Add(vertex.Key, k);
                newReverseVertexIndicies.Add(k, vertex.Key);
                k++;
            }

            nextAvailibleIndex = k;

            for (int i = 0; i < maxSize; i++)
            {
                for (int j = 0; j < maxSize; j++)
                {
                    if (!matrix[i, j].Equals(default(TW))
                        && reverseVertexIndicies.ContainsKey(i)
                         && reverseVertexIndicies.ContainsKey(j))
                    {
                        var newI = newVertexIndicies[reverseVertexIndicies[i]];
                        var newJ = newVertexIndicies[reverseVertexIndicies[j]];

                        newMatrix[newI, newJ] = matrix[i, j];
                    }
                }
            }

            matrix = newMatrix;
            vertexIndicies = newVertexIndicies;
            reverseVertexIndicies = newReverseVertexIndicies;
            maxSize *= 2;
        }

        private void HalfMatrixSize()
        {
            var newMatrix = new TW[maxSize / 2, maxSize / 2];

            var newVertexIndcies = new Dictionary<T, int>();
            var newReverseIndicies = new Dictionary<int, T>();

            int k = 0;

            foreach (var vertex in vertexIndicies)
            {
                newVertexIndcies.Add(vertex.Key, k);
                newReverseIndicies.Add(k, vertex.Key);
                k++;

                nextAvailibleIndex = k;

                for (int i = 0; i < maxSize; i++)
                {
                    for (int j = 0; j < maxSize; j++)
                    {
                        if (!matrix[i, j].Equals(default(TW))
                            && reverseVertexIndicies.ContainsKey(i)
                            && reverseVertexIndicies.ContainsKey(j))
                        {
                            var newI = newVertexIndcies[reverseVertexIndicies[i]];
                            var newJ = newVertexIndcies[reverseVertexIndicies[j]];

                            newMatrix[newI, newJ] = matrix[i, j];
                        }
                    }
                }

                matrix = newMatrix;
                vertexIndicies = newVertexIndcies;
                reverseVertexIndicies = newReverseIndicies;
                maxSize /= 2;
            }
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
