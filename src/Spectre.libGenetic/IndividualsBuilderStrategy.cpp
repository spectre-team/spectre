/*
* IndividualsBuilderStrategy.cpp
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

#include <span.h>
#include "IndividualsBuilderStrategy.h"

namespace Spectre::libGenetic
{
IndividualsBuilderStrategy::IndividualsBuilderStrategy(CrossoverOperator&& crossover,
                                                       MutationOperator&& mutation,
                                                       ParentSelectionStrategy&& parentSelectionStrategy):
    m_Crossover(crossover),
    m_Mutation(mutation),
    m_ParentSelectionStrategy(parentSelectionStrategy)
{
    
}

Generation IndividualsBuilderStrategy::Build(const Generation& old, gsl::span<ScoreType> scores, size_t newSize)
{
    std::vector<Individual> newIndividuals;
    newIndividuals.reserve(newSize);
    for(size_t i = 0; i < newSize; ++i)
    {
        const auto parents = m_ParentSelectionStrategy.next(old, scores);
        const auto child = m_Mutation(m_Crossover(parents.first, parents.second));
        newIndividuals.push_back(child);
    }
    Generation newGeneration(std::move(newIndividuals));
    return std::move(newGeneration);
}
}
