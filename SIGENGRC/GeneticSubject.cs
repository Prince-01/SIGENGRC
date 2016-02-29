using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGENGRC
{
    interface GeneticSubject<T>
    {
        void Cross(T g1, T g2);
        void Mutate(T graph, int mutationProbability);
    }
}
