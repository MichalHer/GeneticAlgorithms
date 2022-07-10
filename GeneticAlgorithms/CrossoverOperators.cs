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
        static public int HammingDistance(BinaryIndividual firstIndividual, BinaryIndividual secondaryIndividual)
        {
            int hammingDistance = 0;
            for (int i = 0; i < firstIndividual.chromosome.Length; i++)
            {
                if (firstIndividual.chromosome[i] != secondaryIndividual.chromosome[i]) hammingDistance++;
            }
            return hammingDistance;
        }
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

        static public BinaryIndividual HalfUniformCrossover(Population population, int chromosomeLength, int elite, ref int treshold)
        {
            Random random = new Random();
            int populationSize = population.Size();
            while (true)
            {
                for (int i = 0; i < (populationSize / 2); i++)
                {
                    BinaryIndividual parent1 = population.GetChromosome(random.Next(0, populationSize));
                    BinaryIndividual parent2 = population.GetChromosome(random.Next(0, populationSize));
                    int hammingDistance = HammingDistance(parent1, parent2);
                    if ((hammingDistance / 2) > treshold)
                    {
                        BinaryIndividual children = new BinaryIndividual(chromosomeLength);
                        int swapsNumber = 0;
                        for (int j = 0; j < chromosomeLength; j++)
                        {
                            if ((parent1.chromosome[j] == parent2.chromosome[j]) || (random.NextDouble() > 0.5) || (swapsNumber > (hammingDistance / 2)))
                            {
                                children.chromosome[j] = parent1.chromosome[j];
                            }
                            else
                            {
                                swapsNumber++;
                                children.chromosome[j] = parent2.chromosome[j];
                            }
                        }
                        return children;
                    }
                }
                treshold--;
                if (treshold == 0)
                {
                    for (int i = elite; i < populationSize; i++)
                    {
                        MutationOperators.BitFlipOperator(population.GetChromosome(i), chromosomeLength, 35);
                    }
                    treshold =(int)(chromosomeLength / 4);
                }
            }
        }

    }
}
