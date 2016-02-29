using System;
using System.Collections.Generic;

namespace SIGENGRC
{
    [Serializable()]
    internal class Vertex
    {
        public Vertex(int id, int color)
        {
            ID = id;
            Color = color;
        }
        public List<Vertex> Connected = new List<Vertex>();
        public int ID { get; set; }
        public int Color { get; set; }
    }
}