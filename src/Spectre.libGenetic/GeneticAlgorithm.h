#pragma once
#include "Generation.h"
#include "IDataset.h"
class IDataset;
class IClassifier;
class ISelection;
class ICrossover;
class IMutation;

namespace Spectre::libGenetic
{
template <class TMutation, class TCrossover, class TClassifier, class TSelection, class TDataset>
class GeneticAlgorithm : public IsDerivedFrom<TDataset, IDataset>, public IsDerivedFrom<TMutation, IMutation>, public IsDerivedFrom<TCrossover, ICrossover>, public IsDerivedFrom<TClassifier, IClassifier>, public IsDerivedFrom<TSelection, ISelection>
{
public:
	GeneticAlgorithm();
	GeneticAlgorithm(TDataset data, TMutation mutation, TCrossover crossover, TSelection selection, TClassifier classifier, long generationSize);
	~GeneticAlgorithm();

private:
	TDataset data;
	TMutation mutation;
	TCrossover crossover;
	TSelection selection;
	TClassifier classifier;
	Generation generationCurrent, generationNew;
	long generationSize;
};
}
