#pragma once
#include "DataTypes.h"

namespace Spectre::libGenetic
{
class Scorer
{
public:
    std::vector<ScoreType> Score(Generation generation);
};
}
