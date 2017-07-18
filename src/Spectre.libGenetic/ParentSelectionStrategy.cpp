#include "ParentSelectionStrategy.h"

namespace Spectre::libGenetic
{
ParentSelectionStrategy::ParentSelectionStrategy(Seed seed):
    m_RandomNumberGenerator(seed)
{
    
}

std::pair<const Individual&, const Individual&> ParentSelectionStrategy::next(const Generation& generation, gsl::span<ScoreType> scores)
{
    std::discrete_distribution<> indexDistribution(scores.begin(), scores.end());
    const auto first = indexDistribution(m_RandomNumberGenerator);
    const auto second = indexDistribution(m_RandomNumberGenerator);
    return std::make_pair(generation[first], generation[second]);
}

}
