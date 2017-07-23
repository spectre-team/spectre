/*
* IndividualsBuilderStrategy.h
* Describes how to create new individual.
*
Copyright 2017 Grzegorz Mrukwa

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

#pragma once
#include "CrossoverOperator.h"
#include "DataTypes.h"
#include "Generation.h"
#include "MutationOperator.h"
#include "ParentSelectionStrategy.h"

namespace Spectre::libGenetic
{
/// <summary>
/// Builds new individuals from old population, basing on specific
/// mutation, crossover and parent selection strategies.
/// </summary>
class IndividualsBuilderStrategy
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="IndividualsBuilderStrategy"/> class.
    /// </summary>
    /// <param name="crossover">The crossover.</param>
    /// <param name="mutation">The mutation.</param>
    /// <param name="parentSelectionStrategy">The parent selection strategy.</param>
    IndividualsBuilderStrategy(CrossoverOperator&& crossover,
                               MutationOperator&& mutation,
                               ParentSelectionStrategy&& parentSelectionStrategy);
    /// <summary>
    /// Builds new generation from the specified old one.
    /// </summary>
    /// <param name="old">The old population.</param>
    /// <param name="scores">The scores of individuals.</param>
    /// <param name="numberOfBuilt">Number of built.</param>
    /// <returns></returns>
    Generation Build(Generation& old, gsl::span<const ScoreType> scores, size_t numberOfBuilt);
    virtual ~IndividualsBuilderStrategy() = default;
private:
    /// <summary>
    /// The crossover operator.
    /// </summary>
    CrossoverOperator m_Crossover;
    /// <summary>
    /// The mutation operator.
    /// </summary>
    MutationOperator m_Mutation;
    /// <summary>
    /// The parent selection strategy.
    /// </summary>
    ParentSelectionStrategy m_ParentSelectionStrategy;
};
}
