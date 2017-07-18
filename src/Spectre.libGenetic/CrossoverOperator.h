#pragma once
#include <random>
#include "DataTypes.h"

namespace Spectre::libGenetic
{
class CrossoverOperator
{
public:
	explicit CrossoverOperator(Seed rngSeed=0);
    virtual ~CrossoverOperator() = default;
    Individual operator()(const Individual& first, const Individual& second);
	
private:
	RandomNumberGenerator m_RandomNumberGenerator;
};
}
