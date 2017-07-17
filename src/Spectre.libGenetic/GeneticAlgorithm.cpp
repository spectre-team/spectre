#include "Selection.h"
#include "Generation.h"
#include "GeneticAlgorithm.h"

namespace Spectre::libGenetic
{
GeneticAlgorithm::GeneticAlgorithm(const Dataset* data, MutationOperator mutation, CrossoverOperator crossover, Selection selection, Scorer scorer, long generationSize)
    : m_Data(data),
      m_Mutation(mutation),
      m_Crossover(crossover),
      m_Selection(selection),
      m_Scorer(scorer),
      m_GenerationSize(generationSize)
{
}
}
