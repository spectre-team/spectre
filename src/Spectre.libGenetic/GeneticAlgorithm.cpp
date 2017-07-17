#include "Selection.h"
#include "Generation.h"
#include "GeneticAlgorithm.h"

namespace Spectre::libGenetic
{
GeneticAlgorithm::GeneticAlgorithm(const Dataset* data, MutationOperator mutation, CrossoverOperator crossover, Selection selection, Classifier classifier, long generationSize)
    : m_Data(data),
      m_Mutation(mutation),
      m_Crossover(crossover),
      m_Selection(selection),
      m_Classifier(classifier),
      m_GenerationSize(generationSize),
      m_CurrentGeneration(Generation(generationSize, 100)),
      m_NewGeneration(Generation(generationSize, 100))
{
}
}
