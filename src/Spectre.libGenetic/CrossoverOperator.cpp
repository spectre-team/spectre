#include "CrossoverOperator.h"
#include "Individual.h"

namespace Spectre::libGenetic
{
CrossoverOperator::CrossoverOperator(Seed rngSeed):
    m_RandomNumberGenerator(rngSeed)
{
}

Individual CrossoverOperator::operator()(const Individual& first, const Individual& second)
{
    std::uniform_int_distribution<size_t> distribution(0, first.size()-1);
    const auto cuttingPoint = distribution(m_RandomNumberGenerator);
    const auto endOfFirst = first.begin() + cuttingPoint;
    const auto beginningOfSecond = second.begin() + cuttingPoint;
    Individual individual;
    individual.emplace_back(first.begin(), endOfFirst);
    individual.emplace_back(beginningOfSecond, second.end());
    return individual;
}
}
