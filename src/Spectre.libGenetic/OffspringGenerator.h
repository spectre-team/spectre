#pragma once
#include <span.h>
#include "DataTypes.h"
#include "Generation.h"
#include "IndividualsBuilderStrategy.h"

namespace Spectre::libGenetic
{
class OffspringGenerator
{
public:
    explicit OffspringGenerator(IndividualsBuilderStrategy&& builder);
    Generation next(Generation old, gsl::span<ScoreType> scores);
    virtual ~OffspringGenerator() = default;
private:
    IndividualsBuilderStrategy m_Builder;
};
}
