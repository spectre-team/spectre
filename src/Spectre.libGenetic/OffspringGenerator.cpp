#include "OffspringGenerator.h"

namespace Spectre::libGenetic
{
OffspringGenerator::OffspringGenerator(IndividualsBuilderStrategy&& builder, PreservationStrategy&& preservationStrategy):
    m_Builder(builder),
    m_PreservationStrategy(preservationStrategy)
{
}

Generation OffspringGenerator::next(const Generation& old, gsl::span<ScoreType>&& scores)
{
    const auto preserved = m_PreservationStrategy.PickBest(old, scores);
    const auto numberOfRemaining = old.size() - preserved.size();
    const auto generated = m_Builder.Build(old, scores, numberOfRemaining);
    return preserved + generated;
}
}
