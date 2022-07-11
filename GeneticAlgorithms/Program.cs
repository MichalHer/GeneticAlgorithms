using GeneticAlgorithms;

void FitnessFunction(BinaryIndividual searchedSolution, BinaryIndividual comparedSolution)
{
    double matchedGenes = 0;
    for (int i = 0; i < searchedSolution.chromosome.Length; i++)
    {
        if (searchedSolution.chromosome[i] == comparedSolution.chromosome[i]) matchedGenes++;
    }
    comparedSolution.fitness = matchedGenes / comparedSolution.chromosome.Length;
}

void MultiPointCrossoverTestProcedure(string selectionMethod,
                         int chromosomeLength,
                         int populationSize,
                         int numberOfParents,
                         int numberOfCandidates,
                         int elite,
                         int mutationProbability,
                         int mutatedPositions,
                         int runsNumber = 10)
{
    for (int run = 0; run < runsNumber; run++)
    {
        //init searched solution
        BinaryIndividual searchedSolution = new BinaryIndividual(chromosomeLength);
        //init population
        Population population = new Population(chromosomeLength, populationSize, populationSize);
        //init childrens' space
        Population childrenPopulation = new Population(populationSize - elite);
        //init parents' space
        Population parents = new Population(numberOfParents);
        //init technical variables
        int epoch = 0;
        int fitnessRepeatStartCondition = 150;
        int fitnessRepeatCounter = fitnessRepeatStartCondition;
        double lastFitness = 0;
        double bestFitness = 0;

        while(fitnessRepeatCounter > 0 && lastFitness != 1)
        {
            //apply fitness function
            for (int i = 0; i < population.Size(); i++)
            {
                BinaryIndividual comparedIndividual = population.GetChromosome(i);
                FitnessFunction(searchedSolution, comparedIndividual);
            }

            //sort population by fitness
            population.SortPopulationByFitness();

            //get best fitness
            bestFitness = population.GetChromosome(0).fitness;

            //check changes
            if (bestFitness == lastFitness) fitnessRepeatCounter--;
            else lastFitness = bestFitness;

            for (int i = 0; i < childrenPopulation.Size(); i++)
            {
                
                //parents selection
                if (selectionMethod == "Rank") ParentSelectionAlgorithms.RankSelection(parents, population, numberOfCandidates);
                else ParentSelectionAlgorithms.RouletteSelection(parents, population);
                //crossover
                BinaryIndividual child = CrossoverOperators.MultiPointCrossover(parents, chromosomeLength);
                //filling children slot
                childrenPopulation.SetChromosome(i, child);
            }

            //swap population with children
            for (int i = elite; i < population.Size(); i++)
            {
                population.SetChromosome(i, childrenPopulation.GetChromosome(i-elite));
            }

            //apply mutation
            for (int i = elite; i < population.Size(); i++)
            {
                MutationOperators.BitFlipOperator(population.GetChromosome(i),mutatedPositions,mutationProbability);
            }
            Console.Write($"{epoch} - {lastFitness}                      \r");
            epoch++;
        }
        Console.WriteLine($"{epoch} - {lastFitness}");
    }
}

void CHCTestProcedure(int chromosomeLength,
                         int populationSize,
                         int elite = 1,
                         int runsNumber = 10)
{
    for (int run = 0; run < runsNumber; run++)
    {
        //init searched solution
        BinaryIndividual searchedSolution = new BinaryIndividual(chromosomeLength);

        //init population
        Population population = new Population(chromosomeLength, populationSize, populationSize);

        //init childrens' space
        Population childrenPopulation = new Population(populationSize - elite);

        //init technical variables
        int epoch = 0;
        int fitnessRepeatStartCondition = 150;
        int fitnessRepeatCounter = fitnessRepeatStartCondition;
        double lastFitness = 0;
        double bestFitness = 0;
        int treshold = (int)(chromosomeLength / 4);

        while (lastFitness != 1)
        {
            //apply fitness function
            for (int i = 0; i < population.Size(); i++)
            {
                BinaryIndividual comparedIndividual = population.GetChromosome(i);
                FitnessFunction(searchedSolution, comparedIndividual);
            }

            //sort population by fitness
            population.SortPopulationByFitness();

            //get best fitness
            bestFitness = population.GetChromosome(0).fitness;

            //check changes
            if (bestFitness == lastFitness) fitnessRepeatCounter--;
            else lastFitness = bestFitness;

            if (bestFitness == 1) break;

            for (int i = 0; i < childrenPopulation.Size(); i++)
            {
                //no parents selection
                BinaryIndividual child = CrossoverOperators.HalfUniformCrossover(population, chromosomeLength, elite, ref treshold);
                //filling children slot
                childrenPopulation.SetChromosome(i, child);
            }

            //swap population with children
            for (int i = elite; i < population.Size(); i++)
            {
                population.SetChromosome(i, childrenPopulation.GetChromosome(i - elite));
            }
            Console.Write($"{epoch} - {lastFitness}                      \r");
            epoch++;
        }
        Console.WriteLine($"{epoch} - {lastFitness}                      ");
    }
}

int chromosomeLength = 800;
int populationSize = 400;
int parents = 4;
int candidates = 40;
int elite = 20;
int mutationProbability = 3;
int mutatedPositions = 100;

MultiPointCrossoverTestProcedure("Roulette", chromosomeLength, populationSize, parents, candidates, elite, mutationProbability, mutatedPositions);
Console.WriteLine("----");
MultiPointCrossoverTestProcedure("Rank", chromosomeLength, populationSize, parents, candidates, elite, mutationProbability, mutatedPositions);
Console.WriteLine("----");


populationSize = 10;

CHCTestProcedure(chromosomeLength, populationSize);