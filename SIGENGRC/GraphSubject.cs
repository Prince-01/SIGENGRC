using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGENGRC
{
    class GraphSubject : GeneticSubject<Graph>
    {
        Random r = new Random();
        public void Cross(Graph g1, Graph g2)
        {
            int LastN = r.Next(g1.VerticesCount());
            for (int i = g1.VerticesCount() - LastN - 1; i < g1.VerticesCount(); i++)
            {
                int c = g1.GetColor(i);
                g1.ChangeColor(i, g2.GetColor(i));
                g2.ChangeColor(i, c);
            }
        }

        public void Mutate(Graph graph, int mutationProbability)
        {
            for (int i = 0; i < graph.VerticesCount(); i++)
            {
                int GraphProbability = r.Next(0, 101);
                if (GraphProbability <= mutationProbability)
                {
                    graph.ChangeColor(i, r.Next(graph.VerticesCount()));
                }
            }
        }
    }
}
