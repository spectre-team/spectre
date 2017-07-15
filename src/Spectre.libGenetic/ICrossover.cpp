#include "ICrossover.h"
#include "Individual.h"

ICrossover::ICrossover(double mutationRate, long rngSeed)
{
	this->crossoverRate = crossoverRate;
	this->rngSeed = rngSeed;
}

ICrossover::~ICrossover()
{
}