#pragma once
#include "Generation.h"
#include "CrossoverOperator.h"
#include "MutationOperator.h"
#include "DataTypes.h"
#include "ParentSelectionStrategy.h"

namespace Spectre::libGenetic
{
class IndividualsBuilderStrategy
{
public:
    IndividualsBuilderStrategy(CrossoverOperator&& crossover,
                               MutationOperator&& mutation,
                               ParentSelectionStrategy&& parentSelectionStrategy);
    Generation Build(const Generation& old, gsl::span<ScoreType> scores, size_t newSize);
    virtual ~IndividualsBuilderStrategy() = default;
private:
    CrossoverOperator m_Crossover;
    MutationOperator m_Mutation;
    ParentSelectionStrategy m_ParentSelectionStrategy;
};
}
