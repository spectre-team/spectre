#pragma once
#include <span.h>
#include "DataTypes.h"
#include "Generation.h"
#include "PreservationStrategy.h"
#include "IndividualsBuilderStrategy.h"

namespace Spectre::libGenetic
{
class OffspringGenerator
{
public:
    explicit OffspringGenerator(IndividualsBuilderStrategy&& builder, PreservationStrategy&& selection);
    Generation next(const Generation& old, gsl::span<ScoreType> scores);
    virtual ~OffspringGenerator() = default;
private:
    IndividualsBuilderStrategy m_Builder;
    PreservationStrategy m_PreservationStrategy;
};
}
