#include "IMutation.h"
#include "Individual.h"

namespace Spectre::libGenetic
{
IMutation::IMutation(double mutationRate, long rngSeed = 0)
{
	this->mutationRate = mutationRate;
	this->rngSeed = rngSeed;
}

IMutation::~IMutation()
{
}
}
