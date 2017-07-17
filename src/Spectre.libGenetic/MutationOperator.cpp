#include "MutationOperator.h"

namespace Spectre::libGenetic
{
MutationOperator::MutationOperator(double mutationRate, double bitSwapRate, Seed rngSeed):
    m_MutationRate(mutationRate),
    m_BitSwapRate(bitSwapRate),
    m_RandomNumberGenerator(rngSeed)
{
    
}

Individual MutationOperator::operator()(Individual&& individual)
{
    std::bernoulli_distribution mutationProbability(m_MutationRate);
    if(mutationProbability(m_RandomNumberGenerator))
    {
        std::bernoulli_distribution swapProbability(m_BitSwapRate);
        const auto swap_random_bits = [&swapProbability, this](bool bit)
        {
            return swapProbability(m_RandomNumberGenerator) ? !bit : bit;
        };
        std::transform(individual.begin(), individual.end(), individual.begin(), swap_random_bits);
    }
    return individual;
}

}
