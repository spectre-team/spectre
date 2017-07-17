#pragma once
#include "Individual.h"

namespace Spectre::libGenetic
{
class MutationOperator
{
public:
	MutationOperator(double mutationRate, long rngSeed = 0);
    virtual ~MutationOperator();
	virtual Individual performMutation(Individual individual);
	virtual bool shouldMutate();
private:
	double mutationRate;
	long rngSeed;
};
}
