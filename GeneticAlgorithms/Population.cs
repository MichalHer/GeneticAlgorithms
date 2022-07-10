using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithms;

namespace GeneticAlgorithms
{
    internal class Population
    {
        public BinaryIndividual[] population;

        public Population(int chromosomeLength, int populationSize, int initialPopulationFilling)
        {
            population = new BinaryIndividual[populationSize];
            for (int i = 0; i < initialPopulationFilling; i++)
            {
                population[i] = new BinaryIndividual(chromosomeLength);
            }
        }

        public Population(int populationSize)
        {
            population = new BinaryIndividual[populationSize];
        }

        public int Size()
        {
            int size = population.Length;
            return size;
        }

        public void SortPopulationByFitness()
        {
            Array.Sort(population, (BinaryIndividual a, BinaryIndividual b) => { return b.fitness.CompareTo(a.fitness); });
        }

        public BinaryIndividual GetChromosome(int position) { return population[position]; }
        public void SetChromosome(int position, BinaryIndividual chromosome) { population[position] = chromosome; }
    }
}
