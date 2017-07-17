#include <span.h>
#include "IndividualsBuilderStrategy.h"

namespace Spectre::libGenetic
{
IndividualsBuilderStrategy::IndividualsBuilderStrategy(CrossoverOperator&& crossover,
                                                       MutationOperator&& mutation,
                                                       ParentSelectionStrategy&& parentSelectionStrategy):
    m_Crossover(crossover),
    m_Mutation(mutation),
    m_ParentSelectionStrategy(parentSelectionStrategy)
{
    
}

Generation IndividualsBuilderStrategy::Build(const Generation& old, gsl::span<ScoreType> scores, size_t newSize)
{
    Generation newGeneration;
    newGeneration.reserve(newSize);
    for(size_t i = 0; i < newSize; ++i)
    {
        const auto parents = m_ParentSelectionStrategy.next(old, scores);
        const auto child = m_Mutation(m_Crossover(parents.first, parents.second));
        newGeneration.push_back(child);
    }
    return newGeneration;
}
}
