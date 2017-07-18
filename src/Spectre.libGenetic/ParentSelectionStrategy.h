#pragma once
#include <span.h>
#include "DataTypes.h"

namespace Spectre::libGenetic
{
class ParentSelectionStrategy
{
public:
    explicit ParentSelectionStrategy(Seed seed = 0);
    std::pair<const Individual&, const Individual&>  next(const Generation& generation, gsl::span<ScoreType> scores);
    virtual ~ParentSelectionStrategy() = default;
private:
    RandomNumberGenerator m_RandomNumberGenerator;
};
}