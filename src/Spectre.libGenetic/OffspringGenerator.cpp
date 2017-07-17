#include "OffspringGenerator.h"

namespace Spectre::libGenetic
{
OffspringGenerator::OffspringGenerator(IndividualsBuilderStrategy&& builder):
    m_Builder(builder)
{
}

Generation OffspringGenerator::next(Generation old, gsl::span<ScoreType> scores)
{
    throw std::exception("Not implemented");
}
}
