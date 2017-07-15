// This is the main DLL file.

#include "ISelection.h"
#include "IMutation.h"
#include "ICrossover.h"
#include "IClassifier.h"
#include "Generation.h"
#include "GeneticAlgorithm.h"
#include "IDataset.h"

template <class TMutation, class TCrossover, class TClassifier, class TSelection, class TDataset>
GeneticAlgorithm<TMutation, TCrossover, TClassifier, TSelection, TDataset>::GeneticAlgorithm()
{
	this->data = data;
	this->mutation = NULL;
	this->crossover = NULL;
	this->selection = NULL;
	this->classifier = NULL;
	this->generationSize = 0;
	this->generationCurrent = NULL;
	this->generationNew = NULL;
}

template <class TMutation, class TCrossover, class TClassifier, class TSelection, class TDataset>
GeneticAlgorithm<TMutation, TCrossover, TClassifier, TSelection, TDataset>::GeneticAlgorithm(TDataset data, TMutation mutation, TCrossover crossover, TSelection selection, TClassifier classifier, long generationSize)
{
	this->data = data;
	this->mutation = mutation;
	this->crossover = crossover;
	this->selection = selection;
	this->classifier = classifier;
	this->generationSize = generationSize;
	this->generationCurrent = Generation(generationSize, 100);
	this->generationNew = NULL;
}

template <class TMutation, class TCrossover, class TClassifier, class TSelection, class TDataset>
GeneticAlgorithm<TMutation, TCrossover, TClassifier, TSelection, TDataset>::~GeneticAlgorithm()
{
}