using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithms;

namespace GeneticAlgorithms
{
    internal class ParentSelectionAlgorithms
    {
        private static int SpinTheWheel(double[] slots)
        {
            Random random = new Random();
            double pointer = random.NextDouble() * slots.Last();
            int k = (int)slots.Length / 2;
            double m = 0.25;

            if (pointer < slots[0]) return 0;

            while (!(slots[k] <= pointer && slots[k + 1] > pointer))
            {
                int nk = (int)(m * slots.Length + 0.5);

                if (slots[k] > pointer) k -= nk;
                else k += nk;

                m *= 0.5;
            }

            return k;
        }

        public static void RankSelection(Population parents, Population population, int numberOfCandidates)
        {
            Random random = new Random();

            for (int i = 0; i < parents.Size(); i++)
            {
                BinaryIndividual bestCandidate = population.GetChromosome(random.Next(0, population.Size()));
                for (int j = 0; j < numberOfCandidates - 1; j++)
                {
                    BinaryIndividual currentCandidate = population.GetChromosome(random.Next(0, population.Size()));
                    if (currentCandidate.fitness > bestCandidate.fitness) bestCandidate = currentCandidate;
                }
                parents.SetChromosome(i,bestCandidate);
            }
        }

        public static void RouletteSelection(Population parents, Population population)
        {
            double[] slots = new double[population.Size()];
            double maximalFitness = population.GetChromosome(0).fitness;
            slots[0] = 1;
            for (int i = 1; i < population.Size(); i++)
            {
                slots[i] = slots[i - 1] + (population.GetChromosome(i).fitness / maximalFitness);
            }

            for (int i = 0; i < parents.Size(); i++)
            {
                parents.SetChromosome(i, population.GetChromosome(SpinTheWheel(slots)));
            }
        }
    }
}
