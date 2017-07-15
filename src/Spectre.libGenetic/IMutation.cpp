#include "IMutation.h"
#include "Individual.h"

IMutation::IMutation(double mutationRate, long rngSeed = 0)
{
	this->mutationRate = mutationRate;
	this->rngSeed = rngSeed;
}

IMutation::~IMutation()
{
}