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
    public class WeightedGraph<T, TW> : IEnumerable<T> where TW : IComparable
    {
        public int VeticesCount => Vertices.Count;
        internal Dictionary<T, WeightedGraphVertex<T, TW>> Vertices { get; set; }

        public WeightedGraph()
        {
            Vertices = new Dictionary<T, WeightedGraphVertex<T, TW>>();
        }

        /// <summary>
        /// Returns a reference vortex.
        /// </summary>
        public WeightedGraphVertex<T, TW> ReferenceVertex
        {
            get
            {
                using (var enumerator = Vertices.GetEnumerator())
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
        /// <returns></returns>
        public WeightedGraphVertex<T, TW> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new WeightedGraphVertex<T, TW>(value);

            Vertices.Add(value, newVertex);

            return newVertex;
        }

        public void RemoveVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!Vertices.ContainsKey(value))
            {
                throw new Exception("Vertex is not in this graph.");
            }

            foreach (var vertex in Vertices[value].Edges)
            {
                vertex.Key.Edges.Remove(Vertices[value]);
            }
            Vertices.Remove(value);
        }

        /// <summary>
        /// Add a new edge to this graph with given weight
        /// and between given source and destination vertex.
        /// Time complexity: O(1).
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="weight"></param>
        public void AddEdge(T source, T destination, TW weight)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(destination))
            {
                throw new Exception("Source or Destination is not in this graph.");
            }
            Vertices[source].Edges.Add(Vertices[destination], weight);
            Vertices[destination].Edges.Add(Vertices[source], weight);
        }

        /// <summary>
        /// Remove given edge.
        /// Time complexity: O(1).
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void RemoveEdges(T source, T destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(destination))
            {
                throw new Exception("Source or destination is not in this graph.");
            }

            if (!Vertices[source].Edges.ContainsKey(Vertices[destination])
                || !Vertices[destination].Edges.ContainsKey(Vertices[source]))
            {
                throw new Exception("Edge does not exist.");
            }
            Vertices[source].Edges.Remove(Vertices[destination]);
            Vertices[destination].Edges.Remove(Vertices[source]);
        }

        /// <summary>
        /// Do we have an edge betwwen source and destination?
        /// Time complexity: O(1).
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool HasEdge(T source, T destination)
        {
            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(destination))
            {
                throw new ArgumentException("Source or destination is not in this graph.");
            }

            return Vertices[source].Edges.ContainsKey(Vertices[destination])
                && Vertices[destination].Edges.ContainsKey(Vertices[source]);
        }

        public List<Tuple<T, TW>> GetAllEdges(T vertex)
        {
            if (!Vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("Vertex is not in this graph.");
            }
            return Vertices[vertex].Edges.Select(x => new Tuple<T, TW>(x.Key.Value, x.Value)).ToList(); // ToList();
        }

        public WeightedGraphVertex<T, TW> FindVertex(T value)
        {
            if (Vertices.ContainsKey(value))
            {
                return Vertices[value];
            }
            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Vertices.Select(x => x.Key).GetEnumerator();
        }
    }

    public class WeightedGraphVertex<T, TW> : IEnumerable<T> where TW : IComparable
    {
        public T Value { get; private set; }

        public Dictionary<WeightedGraphVertex<T, TW>, TW> Edges { get; set; }

        public WeightedGraphVertex(T value)
        {
            Value = value;
            Edges = new Dictionary<WeightedGraphVertex<T, TW>, TW>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Edges.Select(x => x.Key.Value).GetEnumerator();
        }
    }
}
