#include "OffspringGenerator.h"

namespace Spectre::libGenetic
{
OffspringGenerator::OffspringGenerator(IndividualsBuilderStrategy&& builder, Selection&& selection):
    m_Builder(builder),
    m_Selection(selection)
{
}

Generation OffspringGenerator::next(Generation old, gsl::span<ScoreType> scores)
{
    throw std::exception("Not implemented");
}
}
