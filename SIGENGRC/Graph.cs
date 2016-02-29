using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SIGENGRC
{
    [Serializable()]
    public class Graph
    {
        private List<Vertex> Vertices = new List<Vertex>();
        private int NextVertexId = 0;
        public int Rating { get; set; }
        public int ColorsCount { get; set; }


        public void AddVertex(int color)
        {
            Vertices.Add(new Vertex(NextVertexId++, color));
        }
        public void ConnectVertices(int v1, int v2)
        {
            Vertices[v1].Connected.Add(Vertices[v2]);
        }
        public void DisconnectVertices(int v1, int v2)
        {
            //Vertices[v1].Disconnect(Vertices[v2]);
        }
        public void ChangeColor(int v, int c)
        {
            Vertices[v].Color = c;
        }
        public int GetColor(int v)
        {
            return Vertices[v].Color;
        }
        public string PrintColors()
        {
            string wyj = "";
            foreach (var v in Vertices)
                wyj += v.ID + " -> " + v.Color + "\n";

            return wyj.Substring(0, wyj.Length - 1);
        }
        public int VerticesCount()
        {
            return Vertices.Count;
        }
        public void Evaluate()
        {
            HashSet<int> Colors = new HashSet<int>();
            Rating = 1;

            foreach (var vertex in Vertices)
            {
                foreach (var cvertex in vertex.Connected)
                    if (vertex.Color == cvertex.Color)
                        Rating++;
                Colors.Add(vertex.Color);
            }

            ColorsCount = Colors.Count;
            Rating *= Colors.Count;
        }
        public static Graph Parse(string path)
        {
            StreamReader sr = new StreamReader(path);
            string line = "";
            while ((line = sr.ReadLine()).Length == 0 || line[0] != 'p') ;
            Graph g = new Graph();
            for (int i = 0; i < int.Parse(line.Split(' ')[2]); i++)
                g.AddVertex(-1);
            while ((line = sr.ReadLine()) != null && line.Length != 0 && line[0] == 'e')
            {
                var Connection = line.Split(' ');
                g.ConnectVertices(int.Parse(Connection[1]) - 1, int.Parse(Connection[2]) - 1);
            }
            sr.Close();
            return g;
        }
        public Graph DeepCopy()
        {
            Graph newGraph = new Graph();
            newGraph.Vertices = Vertices.ConvertAll(vertex => new Vertex(vertex.ID, vertex.Color));
            for(int i = 0; i < Vertices.Count; i++)
            {
                foreach (var vertex in Vertices[i].Connected)
                {
                    newGraph.ConnectVertices(i, vertex.ID);
                }
            }
            newGraph.ColorsCount = ColorsCount;
            newGraph.Rating = Rating;

            return newGraph;
        }
    }
}
