using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    internal class MutationOperators
    {
        static public void BitFlipOperator(BinaryIndividual individual, int mutatedPositions, int mutationProbability)
        {
            Random random = new Random();
            for (int i = 0; i < mutatedPositions; i++)
            {
                if (random.Next(0, 100) <= mutationProbability)
                {
                    int changedPosition = random.Next(0, individual.chromosome.Length);
                    if (individual.chromosome[changedPosition] == true) individual.chromosome[changedPosition] = false;
                    else individual.chromosome[changedPosition] = true;
                }
            }
            
        }
    }
}
