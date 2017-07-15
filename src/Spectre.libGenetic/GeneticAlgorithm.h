

#include "Generation.h"
class IClassifier;
class ISelection;
class ICrossover;
class IMutation;

class GeneticAlgorithm
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
