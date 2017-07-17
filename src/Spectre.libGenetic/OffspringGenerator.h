#pragma once
#include <span.h>
#include "Generation.h"
#include "IndividualsBuilderStrategy.h"
#include "DataTypes.h"

namespace Spectre::libGenetic
{
class OffspringGenerator
{
public:
    OffspringGenerator(IndividualsBuilderStrategy&& builder);
    Generation next(Generation old, gsl::span<ScoreType> scores);
    virtual ~OffspringGenerator() = default;
private:
    IndividualsBuilderStrategy m_Builder;
};
}
