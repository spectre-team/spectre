#pragma once
#include <span.h>
#include "DataTypes.h"

namespace Spectre::libGenetic
{
class ParentSelectionStrategy
{
public:
    ParentSelectionStrategy(Seed seed = 0);
    std::pair<Individual, Individual> next(const Generation& generation, gsl::span<ScoreType> scores);
    virtual ~ParentSelectionStrategy() = default;
private:
    RandomNumberGenerator m_RandomNumberGenerator;
};
}