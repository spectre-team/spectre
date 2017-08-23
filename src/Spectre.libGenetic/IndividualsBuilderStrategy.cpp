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
#include "Spectre.libException/NullPointerException.h"
#include "InconsistentGenerationAndScoresLengthException.h"
#include "IndividualsBuilderStrategy.h"

namespace Spectre::libGenetic
{
IndividualsBuilderStrategy::IndividualsBuilderStrategy(std::unique_ptr<CrossoverOperator> crossover,
                                                       std::unique_ptr<MutationOperator> mutation,
                                                       std::unique_ptr<ParentSelectionStrategy> parentSelectionStrategy):
    m_Crossover(std::move(crossover)),
    m_Mutation(std::move(mutation)),
    m_ParentSelectionStrategy(std::move(parentSelectionStrategy))
{
    if (m_Crossover != nullptr)
    {
        // @gmrukwa: usual empty execution branch
    }
    else
    {
        throw libException::NullPointerException("crossover");
    }
    if (m_Mutation != nullptr)
    {
        // @gmrukwa: usual empty execution branch
    }
    else
    {
        throw libException::NullPointerException("mutation");
    }
    if (m_ParentSelectionStrategy != nullptr)
    {
        // @gmrukwa: usual empty execution branch
    }
    else
    {
        throw libException::NullPointerException("parentSelectionStrategy");
    }
}

Generation IndividualsBuilderStrategy::Build(Generation &old, gsl::span<const ScoreType> scores, size_t newSize) const
{
    if (old.size() == static_cast<size_t>(scores.size()))
    {
        // @gmrukwa: usual empty execution branch
    }
    else
    {
        throw InconsistentGenerationAndScoresLengthException(old.size(), scores.size());
    }
    std::vector<Individual> newIndividuals;
    newIndividuals.reserve(newSize);
    for (size_t i = 0; i < newSize; ++i)
    {
        const auto parents = m_ParentSelectionStrategy->next(old, scores);
        auto child = (*m_Crossover)(parents.first, parents.second);
        auto mutant = (*m_Mutation)(std::move(child));
        newIndividuals.push_back(std::move(mutant));
    }
    Generation newGeneration(std::move(newIndividuals));
    return std::move(newGeneration);
}
}
