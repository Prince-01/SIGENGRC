using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGENGRC
{
    class Program
    {
        static void Main(string[] args)
        {
            AG ag = new AG();
            //ag.MutationProbability = 1;
            //ag.CrossingProbability = 90;
            ag.Perform();
        }
    }
}
