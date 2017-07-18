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
    std::uniform_int_distribution<size_t> distribution(0, first.size());
    const auto cuttingPoint = distribution(m_RandomNumberGenerator);
    const auto endOfFirst = first.begin() + cuttingPoint;
    const auto beginningOfSecond = second.begin() + cuttingPoint;
    std::vector<bool> phenotype;
    phenotype.reserve(first.size());
    phenotype.insert(phenotype.end(), first.begin(), endOfFirst);
    phenotype.insert(phenotype.end(), beginningOfSecond, second.end());
    Individual individual(std::move(phenotype));
    return std::move(individual);
}
}
