#pragma once
#include "DataTypes.h"

namespace Spectre::libGenetic
{
class FitnessFunction
{
public:
    virtual ScoreType operator()(const Individual& individual) = 0;
    virtual ~FitnessFunction() = default;
};
}
