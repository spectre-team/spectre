// This is the main DLL file.

#include "ISelection.h"
#include "IMutation.h"
#include "ICrossover.h"
#include "IClassifier.h"
#include "Generation.h"
#include "GeneticAlgorithm.h"

GeneticAlgorithm::GeneticAlgorithm(IDataset data, IMutation mutation, ICrossover crossover, ISelection selection, IClassifier classifier, long generationSize)
{
	this.data = data;
	this->mutation = mutation;
	this->crossover = crossover;
	this->selection = selection;
	this->classifier = classifier;
	this->generationSize = generationSize;
	this->generationCurrent = new Generation(generationSize, 100);
	this->generationNew = NULL;
}

GeneticAlgorithm::~GeneticAlgorithm()
{
}