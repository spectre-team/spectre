#pragma once
#include "Individual.h"

class IMutation
{
public:
	IMutation(double mutationRate, long rngSeed = 0);
	~IMutation();
	virtual Individual performMutation(Individual individual);
	virtual bool shouldMutate();
private:
	double mutationRate;
	long rngSeed;
};
