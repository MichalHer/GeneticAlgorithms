using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithms;

namespace GeneticAlgorithms
{
    internal class BinaryIndividual
    {
        public bool [] chromosome;
        public double fitness;
        public double chromosomeLength;

        public void PrintChromosome()
        {
            StringBuilder sb = new StringBuilder("[",1+(chromosome.Length*2));
            for (int i = 0; i < chromosome.Length; i++)
            {
                sb.Append(chromosome[i]);
                if(i < chromosome.Length - 1) { sb.Append('\u002C'); }
            }
            sb.Append(']');
            Console.WriteLine(sb.ToString());
        }
        public BinaryIndividual(int chromosomeLength)
        {
            chromosomeLength = chromosomeLength;
            chromosome = new bool[chromosomeLength];
            Random random = new Random();
            for (int i = 0; i < chromosomeLength; i++)
            {
                int trait = random.Next(0,2);
                chromosome[i] = Convert.ToBoolean(trait);
            }
        }
    }
}
