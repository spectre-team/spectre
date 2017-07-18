#include "PreservationStrategy.h"
#include "Sorting.h"

namespace Spectre::libGenetic
{
PreservationStrategy::PreservationStrategy(double preservationRate):
    m_PreservationRate(preservationRate)
{
}

Generation PreservationStrategy::PickBest(const Generation& generation, gsl::span<ScoreType> scores)
{
    const auto indices = Sorting::indices(scores);
    const auto numberOfPreserved = static_cast<size_t>(std::min(m_PreservationRate * generation.size(), 1.0));
    std::vector<Individual> bestIndividuals;
    bestIndividuals.reserve(numberOfPreserved);
    std::transform(indices.begin(), indices.begin() + numberOfPreserved, std::back_inserter(bestIndividuals), [&generation](size_t index) { return generation[index]; });
    Generation newGeneration(std::move(bestIndividuals));
    return newGeneration;
}

}
