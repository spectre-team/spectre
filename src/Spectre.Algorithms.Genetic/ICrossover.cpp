#include "ICrossover.h"
#include "Individual.h"

public class ICrossover
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

ICrossover::ICrossover(double mutationRate, long rngSeed)
{
	this->crossoverRate = crossoverRate;
	this->rngSeed = rngSeed;
}

ICrossover::~ICrossover()
{
}