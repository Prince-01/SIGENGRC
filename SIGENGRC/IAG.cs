using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGENGRC
{
    interface IAG
    {
        void Perform();
        Graph[] Initialize(int n);
        void Evaluate(Graph[] lg);
        Graph[] Selection(Graph[] lg);
        void Crossover(Graph[] lg);
        void Mutation(Graph[] lg);
    }
}
