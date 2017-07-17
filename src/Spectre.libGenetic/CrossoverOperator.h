#pragma once
//#include "Individual.h"
#include "DataTypes.h"

namespace Spectre::libGenetic
{
class CrossoverOperator
{
public:
	CrossoverOperator(double mutationRate, long rngSeed);
    virtual ~CrossoverOperator();
	virtual Individual performCrossover(Individual individual);
	virtual bool shouldCrossover();

private:
	double crossoverRate;
	long rngSeed;
};
}
