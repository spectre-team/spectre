#include "Generation.h"
#include "GeneticAlgorithm.h"

namespace Spectre::libGenetic
{
GeneticAlgorithm::GeneticAlgorithm(OffspringGenerator&& offspringGenerator, Scorer&& scorer, StopCondition&& stopCondition)
    : m_OffspringGenerator(std::move(offspringGenerator)),
      m_Scorer(std::move(scorer)),
      m_StopCondition(std::move(stopCondition))
{
}

Generation GeneticAlgorithm::evolve(Generation&& generation)
{
    while(!m_StopCondition())
    {
        auto scores = m_Scorer.Score(generation);
        generation = m_OffspringGenerator.next(generation, scores);
    }
    return generation;
}
}
