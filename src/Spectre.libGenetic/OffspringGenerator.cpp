#include "OffspringGenerator.h"

namespace Spectre::libGenetic
{
OffspringGenerator::OffspringGenerator(IndividualsBuilderStrategy&& builder, PreservationStrategy&& preservationStrategy):
    m_Builder(builder),
    m_PreservationStrategy(preservationStrategy)
{
}

Generation OffspringGenerator::next(Generation&& old, gsl::span<ScoreType> scores)
{
    auto preserved = m_PreservationStrategy.PickBest(old, scores);
    const auto numberOfRemaining = old.size() - preserved.size();
    const auto generated = m_Builder.Build(old, scores, numberOfRemaining);
    preserved.insert(preserved.end(), generated.begin(), generated.end());
    return preserved;
}
}
