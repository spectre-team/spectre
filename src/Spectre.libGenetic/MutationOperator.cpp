#include "MutationOperator.h"

namespace Spectre::libGenetic
{
MutationOperator::MutationOperator(double mutationRate, Seed rngSeed):
    m_MutationRate(mutationRate),
    m_RandomNumberGenerator(rngSeed)
{
    
}

Individual MutationOperator::operator()(Individual&& individual)
{
    std::bernoulli_distribution swapProbability(m_MutationRate);
    const auto swap_bits = [&swapProbability, this](bool bit)
    {
        return swapProbability(m_RandomNumberGenerator) ? !bit : bit;
    };
    std::transform(individual.begin(), individual.end(), individual.begin(), swap_bits);
    return individual;
}

}
