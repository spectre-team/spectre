#include "MutationOperator.h"

namespace Spectre::libGenetic
{
MutationOperator::MutationOperator(double mutationRate, long rngSeed)
{
	this->mutationRate = mutationRate;
	this->rngSeed = rngSeed;
}

MutationOperator::~MutationOperator()
{
}
}
