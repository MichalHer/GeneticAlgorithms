using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithms;

namespace GeneticAlgorithms
{
    internal class CrossoverOperators
    {
        static public BinaryIndividual MultiPointCrossover(Population parents, int chromosomeLength)
        {
            Random random = new Random();
            int parentsSize = parents.Size();
            int[] splitPoints = new int[parentsSize + 1];
            splitPoints[0] = 0;
            for (int i = 1; i < parentsSize; i++)
            {
                splitPoints[i] = random.Next(splitPoints[i - 1] + 1, (chromosomeLength / parentsSize) * i);
            }
            splitPoints[parentsSize] = chromosomeLength;

            BinaryIndividual children = new BinaryIndividual(chromosomeLength);
            for (int j = 0; j < parentsSize; j++)
            {
                BinaryIndividual parent = parents.GetChromosome(j);
                for (int s = splitPoints[j]; s < splitPoints[j + 1]; s++)
                {
                    children.chromosome[s] = parent.chromosome[s];
                }
            }
            return children;
        }
    }
}
