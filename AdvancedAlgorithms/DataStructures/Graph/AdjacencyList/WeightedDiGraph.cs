using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// A weighted graph implementation.
    /// IEnumerable enumerates all verticles.
    /// </summary>
    public class WeightedDiGraph<T, TW> : IEnumerable<T> where TW : IComparable
    {
        public int VerticleCount => Verticles.Count;
        internal Dictionary<T, WeightedDiGraphVertex<T, TW>> Verticles { get; set; }

        public WeightedDiGraph()
        {
            Verticles = new Dictionary<T, WeightedDiGraphVertex<T, TW>>();
        }

        /// <summary>
        /// Returns a reference vertex
        /// Time complexity: O/1
        /// </summary>
        /// <returns></returns>
        public WeightedDiGraphVertex<T, TW> ReferenceVertex
        {
            get
            {
                using (var enumerator = Verticles.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        return enumerator.Current.Value;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Add a new vertex to this graph
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WeightedDiGraphVertex<T, TW> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentException();
            }

            var newVertex = new WeightedDiGraphVertex<T, TW>(value);

            Verticles.Add(value, newVertex);

            return newVertex;
        }

        /// <summary>
        /// Remove the given vertex.
        /// Time complexity: O(V) where V is the number of verticles.
        /// </summary>
        public void RemoveVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!Verticles.ContainsKey(value))
            {
                throw new Exception("Vetex is not in this  graph.");
            }

            foreach (var vertex in Verticles[value].InEdges)
            {
                vertex.Key.OutEdges.Remove(Verticles[value]);
            }

            foreach (var vertex in Verticles[value].OutEdges)
            {
                vertex.Key.InEdges.Remove(Verticles[value]);
            }
            Verticles.Remove(value);
        }

        /// <summary>
        /// Add a new edge to this graph
        /// </summary>
        /// <param name="value"></param>
        public void AddEdge(T source, T destination, T weight)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentException();
            }

            if (!Verticles.ContainsKey(source)
                || !Verticles.ContainsKey(destination))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (Verticles[source].OutEdges.ContainsKey(Verticles[destination])
                || Verticles[destination].InEdges.ContainsKey(Verticles[source]))
            {
                throw new Exception("Edge already exists.");
            }
            Verticles[source].OutEdges.Add(Verticles[destination], weight);
            Verticles[destination].InEdges.Add(Verticles[source], weight);
        }

        /// <summary>
        /// Remove the given edge from this graph
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void RemoveEdge(T source, T destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentException();
            }

            if (!Verticles.ContainsKey(source) || !Verticles.ContainsKey(destination))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!Verticles[source].OutEdges.ContainsKey(Verticles[destination])
                || !Verticles[destination].InEdges.ContainsKey(Verticles[source]))
            {
                throw new Exception("Edge does not exist.");
            }
            Verticles[source].OutEdges.Remove(Verticles[destination]);
            Verticles[destination].InEdges.Remove(Verticles[source]);
        }

        /// <summary>
        /// Do we have an edge between the source and the destination.
        /// Time complexity: O(1).
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool HasEdge(T source, T destination)
        {
            if (!Verticles.ContainsKey(source) || !Verticles.ContainsKey(destination))
            {
                throw new ArgumentException("Source or Destination is not in this graph.");
            }
            return Verticles[source].OutEdges.ContainsKey(Verticles[destination])
                && Verticles[destination].InEdges.ContainsKey(Verticles[source]);
        }

        public IEnumerable<Tuple<T, TW>> OutEdges(T vertex)
        {
            if (!Verticles.ContainsKey(vertex))
            {
                throw new ArgumentException("Vertex is not in this graph.");
            }
            return Verticles[vertex].OutEdges.Select(x => new Tuple<T, TW>(x.Key.Value, x.Value));
        }

        public IEnumerable<Tuple<T, TW>> InEdges(T vertex)
        {
            if (!Verticles.ContainsKey(vertex))
            {
                throw new ArgumentException("Vertex is not in this graph.");
            }
            return Verticles[vertex].InEdges.Select(x => new Tuple<T, TW>(x.Key.Value, x.Value));
        }

        /// <summary>
        /// Returns the vertex with a given value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WeightedDiGraphVertex<T, TW> FindVertex(T value)
        {
            if (Verticles.ContainsKey(value))
            {
                return Verticles[value];
            }
            return null;
        }

        /// <summary>
        /// Clone this graph.
        /// </summary>
        /// <returns></returns>
        internal WeightedDiGraph<T, TW> Clone()
        {
            var newGraph = WeightedDiGraph<T, TW>();

            foreach (var vertex in Verticles)
            {
                
            }

            foreach (var vertex in Verticles)
            {
                foreach (var edge in vertex.Value.OutEdges)
                {

                }
            }
            return newGraph;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Verticles.Select(x => x.Key).GetEnumerator();
        }
    }

    public class WeightedDiGraphVertex<T, TW> : IEnumerable<T> where TW : IComparable
    {
        public T Value { get; set; }
        public Dictionary<WeightedDiGraphVertex<T, TW>, TW> OutEdges { get; set; }
        public Dictionary<WeightedDiGraphVertex<T, TW>, TW> InEdges { get; set; }

        public WeightedDiGraphVertex(T value)
        {
            Value = value;

            OutEdges = new Dictionary<WeightedDiGraphVertex<T, TW>, TW>();
            InEdges = new Dictionary<WeightedDiGraphVertex<T, TW>, TW>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return OutEdges.Select(x => x.Key.Value).GetEnumerator();
        }
    }
}
