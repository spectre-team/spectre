#include <span.h>
#include "IndividualsBuilderStrategy.h"

namespace Spectre::libGenetic
{
IndividualsBuilderStrategy::IndividualsBuilderStrategy(CrossoverOperator&& crossover, MutationOperator&& mutation):
    m_Crossover(crossover),
    m_Mutation(mutation)
{
    
}

Generation IndividualsBuilderStrategy::build(Generation old, gsl::span<ScoreType> scores, size_t newSize)
{
    throw std::exception("Not implemented");
}
}
