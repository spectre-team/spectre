#pragma once
//#include "Individual.h"
#include "DataTypes.h"

namespace Spectre::libGenetic
{
class MutationOperator
{
public:
	explicit MutationOperator(double mutationRate, double bitSwapRate, Seed rngSeed=0);
    Individual operator()(Individual&& individual);
    virtual ~MutationOperator() = default;
private:
	const double m_MutationRate;
    const double m_BitSwapRate;
    RandomNumberGenerator m_RandomNumberGenerator;
};
}
