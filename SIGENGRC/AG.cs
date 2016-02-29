using System;
using System.Collections.Generic;
using System.Linq;

namespace SIGENGRC
{
    class AG : IAG
    {
        Random r = new Random();
        GeneticSubject<Graph> gs = new GraphSubject();
        public Graph[] Initialize(int n)
        {
            Graph first = Graph.Parse("graph.txt");
            List<Graph> Population = new List<Graph>();
            for (int i = 0; i < n; i++)
            {
                Graph g = first.DeepCopy();
                for (int j = 0; j < first.VerticesCount(); j++)
                    g.ChangeColor(j, r.Next(first.VerticesCount()));
                Population.Add(g);
            }
            return Population.ToArray();
        }
        public void Evaluate(Graph[] lg)
        {
            foreach (var graph in lg)
            {
                graph.Evaluate();
            }
        }
        public Graph[] Selection(Graph[] lg)
        {
            lg = (from g in lg orderby g.Rating select g).Take(lg.Length / 8).ToArray();
            double[] ratings_reverses = lg.Select(g => 1.0 / g.Rating).ToArray();
            double whole = ratings_reverses.Sum();
            Graph[] Population = new Graph[lg.Length * 8];
            for (int i = 0; i < lg.Length * 8; i++)
            {
                double current = r.Next(101) * whole;
                double sum = 0.0;
                for (int j = 0; j < ratings_reverses.Length; j++)
                {
                    sum += ratings_reverses[j];// / whole;
                    if (current <= sum * 100)
                    {
                        Population[i] = lg[j].DeepCopy();
                        break;
                    }
                }
            }
            return Population;
        }
        public void Crossover(Graph[] lg)
        {
            int CrossingProbability = r.Next(75, 101);

            for (int i = 0; i < lg.Length / 2; i++)
            {
                int PairProbability = r.Next(101);
                if (PairProbability < CrossingProbability)
                    gs.Cross(lg[i], lg[i + 1]);
            }
        }
        public void Mutation(Graph[] lg)
        {
            int MutationProbability = r.Next(0, 2);
            if (MutationProbability == 0)
                return;
            foreach (var graph in lg)
            {
                gs.Mutate(graph, MutationProbability);
            }
        }
        public void Perform()
        {
            int t = 0;
            Graph[] population = Initialize(200);
            Evaluate(population);
            Graph BestEver = population[0];
            while (t < 10000)
            {
                population = Selection(population);
                Crossover(population);
                Mutation(population);
                Evaluate(population);

                Graph best = population[0];
                foreach (var graph in population)
                {
                    if (best.Rating > graph.Rating)
                        best = graph;
                }

                if (BestEver.Rating > best.Rating)
                {
                    BestEver = best.DeepCopy();
                    Console.WriteLine(BestEver.Rating);
                    Console.WriteLine(BestEver.ColorsCount);
                    Console.WriteLine("    " + t);
                }
                t++;
            }

            Console.ReadKey();
        }
    }
}
