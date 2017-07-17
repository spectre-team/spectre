#pragma once
#include "Generation.h"
#include "DataTypes.h"

namespace Spectre::libGenetic
{
class Scorer
{
public:
    std::vector<ScoreType> Score(Generation generation);
};
}
