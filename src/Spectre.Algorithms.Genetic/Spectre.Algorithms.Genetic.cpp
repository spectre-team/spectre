// This is the main DLL file.

#include "stdafx.h"

#include "Spectre.Algorithms.Genetic.h"
#include "ISelection.h"
#include "IMutation.h"
#include "ICrossover.h"
#include "IClassifier.h"
#include "Generation.h"

public class GeneticAlgorithm
{
public:
	GeneticAlgorithm(IDataset data, IMutation mutation, ICrossover crossover, ISelection selection, IClassifier classifier, long generationSize);
	~GeneticAlgorithm();

private:
	IDataset data;
	IMutation mutation;
	ICrossover crossover;
	ISelection selection;
	IClassifier classifier;
	Generation generationCurrent, generationNew;
	long generationSize;
};

GeneticAlgorithm::GeneticAlgorithm(IDataset data, IMutation mutation, ICrossover crossover, ISelection selection, IClassifier classifier, long generationSize)
{
	this.data = data;
	this->mutation = mutation;
	this->crossover = crossover;
	this->selection = selection;
	this->classifier = classifier;
	this->generationSize = generationSize;
}

GeneticAlgorithm::~GeneticAlgorithm()
{
}