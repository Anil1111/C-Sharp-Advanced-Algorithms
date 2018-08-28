using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms.DataStructures.Graph.AdjacencyList
{
    public class Graph<T> : IEnumerable<T>
    {
        public int VerticlesCount => Verticles.Count;
        internal Dictionary<T, GraphVertex<T>> Verticles { get; set; }

        public Graph()
        {
            Verticles = new Dictionary<T, GraphVertex<T>>();
        }

        /// <summary>
        /// Returns a reference vertex.
        /// Time complexity: O/1
        /// </summary>
        public GraphVertex<T> ReferenceVertex
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
        /// Add a new vertex to graph.
        /// Time complexity: O/1
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GraphVertex<T> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new GraphVertex<T>(value);

            Verticles.Add(value, newVertex);

            return newVertex;
        }

        /// <summary>
        /// Remove an existing vertex from this graph.
        /// Time complexity: O(V) where V is the number of verticles.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public void RemoveVertex(T vertex)
        {
            if (vertex == null)
            {
                throw new ArgumentNullException();
            }

            if (!Verticles.ContainsKey(vertex))
            {
                throw new Exception("Vertex is not in this graph.");
            }

            foreach (var v in Verticles[vertex].Edges)
            {
                v.Edges.Remove(Verticles[vertex]);
            }
            Verticles.Remove(vertex);
        }

        /// <summary>
        /// Add an edge to this graph.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void AddEdge(T source, T destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentException();
            }

            if (!Verticles.ContainsKey(source) || !Verticles.ContainsKey(destination))
            {
                throw new Exception("Source or Destination Vwertex is not in this graph");
            }

            if (Verticles[source].Edges.Contains(Verticles[destination])
                || Verticles[destination].Edges.Contains(Verticles[source]))
            {
                throw new Exception("Edge already exits.");
            }
            Verticles[source].Edges.Add(Verticles[destination]);
            Verticles[destination].Edges.Add(Verticles[source]);
        }

        /// <summary>
        /// Remove an edge from this graph.
        /// </summary>
        /// <param name="scource"></param>
        /// <param name="destination"></param>
        public void RemoveEdge(T source, T destination)
        {
            if (source == null | destination == null)
            {
                throw new ArgumentException();
            }

            if (!Verticles.ContainsKey(source) || !Verticles.ContainsKey(destination))
            {
                throw new Exception("Source or destination Vertex is not in this graph.");
            }

            if (!Verticles[source].Edges.Contains(Verticles[destination])
                || !Verticles[destination].Edges.Contains(Verticles[source]))
            {
                throw new Exception("Edge does not exist");
            }
            Verticles[source].Edges.Remove(Verticles[destination]);
            Verticles[destination].Edges.Remove(Verticles[source]);
        }

        /// <summary>
        /// Do we have an edge between the given source and destination?
        /// Time complexity: O/1
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
            return Verticles[source].Edges.Contains(Verticles[destination])
                && Verticles[destination].Edges.Contains(Verticles[source]);
        }

        public IEnumerable<T> Edges(T vertex)
        {
            if (!Verticles.ContainsKey(vertex))
            {
                throw new ArgumentException("Vertex is not in this graph.");
            }
            return Verticles[vertex].Edges.Select(x => x.Value);
        }

        public GraphVertex<T> FindVertex(T value)
        {
            if (Verticles.ContainsKey(value))
            {
                return Verticles[value];
            }
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Verticles.Select(x => x.Key).GetEnumerator();
        }
    }

    /// <summary>
    /// Graph vertex for adjacency list graph implementation.
    /// IEnumerable enumerates all the outgoing edge destination verticles
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GraphVertex<T> : IEnumerable<T>
    {
        public T Value { get; set; }

        public HashSet<GraphVertex<T>> Edges { get; set; }

        public GraphVertex(T value)
        {
            Value = value;
            Edges = new HashSet<GraphVertex<T>>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Edges.Select(x => x.Value).GetEnumerator();
        }
    }
}
