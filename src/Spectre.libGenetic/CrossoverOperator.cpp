#include "CrossoverOperator.h"
#include "Individual.h"

namespace Spectre::libGenetic
{
CrossoverOperator::CrossoverOperator(double crossoverRate, long rngSeed)
{
	this->crossoverRate = crossoverRate;
	this->rngSeed = rngSeed;
}

CrossoverOperator::~CrossoverOperator()
{
}
}
