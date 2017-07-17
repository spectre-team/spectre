#pragma once
#include "Generation.h"
#include "CrossoverOperator.h"
#include "MutationOperator.h"
#include "DataTypes.h"

namespace Spectre::libGenetic
{
class IndividualsBuilderStrategy
{
public:
    IndividualsBuilderStrategy(CrossoverOperator&& crossover, MutationOperator&& mutation);
    Generation Build(const Generation& old, gsl::span<ScoreType> scores, size_t newSize);
    virtual ~IndividualsBuilderStrategy() = default;
private:
    CrossoverOperator m_Crossover;
    MutationOperator m_Mutation;
};
}
