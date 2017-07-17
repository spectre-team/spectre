#pragma once
//#include "Individual.h"
#include "DataTypes.h"
#include <random>

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
