using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// A directed graph implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DiGraph<T> : IEnumerable<T>
    {
        public int VerticlesCount => Verticles.Count;
        internal Dictionary<T, DiGraphVertex<T>> Verticles { get; set; }

        public DiGraph()
        {
            Verticles = new Dictionary<T, DiGraphVertex<T>>();
        }

        /// <summary>
        /// Return a reference vertex to start traversing Vertices
        /// </summary>
        public DiGraphVertex<T> ReferenceVertex
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
        /// <returns></returns>
        public DiGraphVertex<T> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new DiGraphVertex<T>(value);

            Verticles.Add(value, newVertex);

            return newVertex;
        }

        /// <summary>
        /// Remove an existing vertex from graph
        /// </summary>
        /// <param name="value"></param>
        public void RemoveVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!Verticles.ContainsKey(value))
            {
                throw new Exception("vertex not in this graph");
            }

            foreach (var vertex in Verticles[value].InEdges)
            {
                vertex.OutEdges.Remove(Verticles[value]);
            }

            foreach (var vertex in Verticles[value].OutEdges)
            {
                vertex.InEdges.Remove(Verticles[value]);
            }
            Verticles.Remove(value);
        }

        /// <summary>
        /// Add an edge from source to destination vertex.
        /// Time complexity: O/1.
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
                throw new Exception("Source or Destination Vertex not in this graph.");
            }

            if (!Verticles[source].OutEdges.Contains(Verticles[destination]) || Verticles[destination].InEdges.Contains(Verticles[source]))
            {
                throw new Exception("Edge already exists");
            }
            Verticles[source].OutEdges.Add(Verticles[destination]);
            Verticles[destination].InEdges.Add(Verticles[source]);
        }

        /// <summary>
        /// Remove an existing edge between source and destination
        /// </summary>
        public void RemoveEdge(T source, T destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentException();
            }

            if (!Verticles.ContainsKey(source) || !Verticles.ContainsKey(destination))
            {
                throw new Exception("Source or Destination Vertex not in this graph");
            }

            if (!Verticles[source].OutEdges.Contains(Verticles[destination]) ||
                !Verticles[destination].InEdges.Contains(Verticles[source]))
            {
                throw new Exception("Edge does not exist.");
            }
            Verticles[source].OutEdges.Remove(Verticles[destination]);
            Verticles[destination].InEdges.Remove(Verticles[source]);
        }

        /// <summary>
        /// Do we have an edge between the given source and destination?
        /// </summary>
        /// <returns></returns>
        public bool HasEdge(T source, T destination)
        {
            if (!Verticles.ContainsKey(source) || !Verticles.ContainsKey(destination))
            {
                throw new ArgumentException("Source or Destination is not in this graph.");
            }
            return Verticles[source].OutEdges.Contains(Verticles[destination])
                && Verticles[destination].InEdges.Contains(Verticles[source]);
        }

        public IEnumerable<T> OutEdges(T vertex)
        {
            if (!Verticles.ContainsKey(vertex))
            {
                throw new ArgumentException("Vertex is not in this graph.");
            }
            return Verticles[vertex].OutEdges.Select(x => x.Value);
        }

        public IEnumerable<T> InEdges(T vertex)
        {
            if (!Verticles.ContainsKey(vertex))
            {
                throw new ArgumentException("Vertex not in this graph.");
            }
            return Verticles[vertex].InEdges.Select(x => x.Value);
        }

        /// <summary>
        /// Returns the vertex object with given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DiGraphVertex<T> FindVertex(T value)
        {
            return Verticles.ContainsKey(value) ? Verticles[value] : null;
        }

        /// <summary>
        /// Clones this graph
        /// </summary>
        /// <returns></returns>
        internal DiGraph<T> Clone()
        {
            var newGraph = new DiGraph<T>();

            foreach (var vertex in Verticles)
            {
                newGraph.AddVertex(vertex.Key);
            }

            foreach (var vertex in Verticles)
            {
                foreach (var edge in vertex.Value.OutEdges)
                {
                    newGraph.AddEdge(vertex.Value.Value, edge.Value);
                }
            }
            return newGraph;
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

    public class DiGraphVertex<T> : IEnumerable<T>
    {
        public T Value { get; set; }
    
        public HashSet<DiGraphVertex<T>> OutEdges { get; set; }
        public HashSet<DiGraphVertex<T>> InEdges { get; set; }

        public DiGraphVertex(T value)
        {
            Value = value;
            OutEdges = new HashSet<DiGraphVertex<T>>();
            InEdges = new HashSet<DiGraphVertex<T>>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return OutEdges.Select(x => x.Value).GetEnumerator();
        }
    }
}
