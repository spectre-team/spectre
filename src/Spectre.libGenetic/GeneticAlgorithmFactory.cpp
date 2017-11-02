/*
* GeneticAlgorithmFactory.cpp
* Builds different versions of GeneticAlgorithm
*
Copyright 2017 Spectre Team

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

#include "GeneticAlgorithmFactory.h"


namespace Spectre::libGenetic
{
GeneticAlgorithmFactory::GeneticAlgorithmFactory(double mutationRate,
                                                 double bitSwapRate,
                                                 double preservationRate,
                                                 unsigned generationsNumber,
                                                 unsigned numberOfCores,
                                                 size_t minimalFillup,
                                                 size_t maximalFillup):
    m_MutationRate(mutationRate),
    m_BitSwapRate(bitSwapRate),
    m_PreservationRate(preservationRate),
    m_GenerationsNumber(generationsNumber),
    m_NumberOfCores(numberOfCores),
    m_MinimalFillup(minimalFillup),
    m_MaximalFillup(maximalFillup)
{ }


std::unique_ptr<GeneticAlgorithm> GeneticAlgorithmFactory::BuildDefault(std::unique_ptr<FitnessFunction> fitnessFunction,
                                                                        Seed seed) const
{
    auto crossoverOperator = std::make_unique<CrossoverOperator>(seed, m_MinimalFillup, m_MaximalFillup);
    auto mutationOperator = std::make_unique<MutationOperator>(m_MutationRate, m_BitSwapRate, seed, m_MinimalFillup, m_MaximalFillup);
    auto parentSelectionStrategy = std::make_unique<ParentSelectionStrategy>(seed);
    auto individualsBuilderStrategy = std::make_unique<IndividualsBuilderStrategy>(std::move(crossoverOperator), std::move(mutationOperator), std::move(parentSelectionStrategy));

    auto preservationStrategy = std::make_unique<PreservationStrategy>(m_PreservationRate);

    auto offspringGenerator = std::make_unique<OffspringGenerator>(std::move(individualsBuilderStrategy), std::move(preservationStrategy));

    auto scorer = std::make_unique<Scorer>(std::move(fitnessFunction), m_NumberOfCores);
    auto stopCondition = std::make_unique<StopCondition>(m_GenerationsNumber);

    auto algorithm = std::make_unique<GeneticAlgorithm>(std::move(offspringGenerator), std::move(scorer), std::move(stopCondition));

    return algorithm;
}
}
