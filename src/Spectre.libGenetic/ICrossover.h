#pragma once
#include "Individual.h"

namespace Spectre::libGenetic
{
class ICrossover
{
public:
	ICrossover(double mutationRate, long rngSeed);
	~ICrossover();
	virtual Individual performCrossover(Individual individual);
	virtual bool shouldCrossover();

private:
	double crossoverRate;
	long rngSeed;
};
}
