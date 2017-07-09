#include "IMutation.h"
#include "Individual.h"

public class IMutation
{
public:
	IMutation(double mutationRate, long rngSeed=0);
	~IMutation();
	virtual Individual performMutation(Individual individual);
	virtual bool shouldMutate();
private:
	double mutationRate;
	long rngSeed;
};

IMutation::IMutation(double mutationRate, long rngSeed = 0)
{
	this->mutationRate = mutationRate;
	this->rngSeed = rngSeed;
}

IMutation::~IMutation()
{
}