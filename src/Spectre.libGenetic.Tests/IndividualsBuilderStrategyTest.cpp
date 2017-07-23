/*
* IndividualsBuilderStrategyTest.cpp
* Tests IndividualsBuilderStrategy
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

#include <gtest/gtest.h>
#include "Spectre.libGenetic/IndividualsBuilderStrategy.h"
#include "MockCrossoverOperator.h"
#include "MockMutationOperator.h"
#include "MockParentSelectionStrategy.h"
#include "Spectre.libGenetic/InconsistentGenerationAndScoresLengthException.h"

namespace
{
using namespace Spectre::libGenetic;
using namespace ::testing;

class IndividualsBuilderTest: public ::testing::Test
{
public:
    IndividualsBuilderTest():
        generation(std::vector<Individual>(generationData))
    {}
protected:
    const std::vector<Individual> generationData{ Individual({ true, true }), Individual({ false, true }) };
    Generation generation;
    const std::vector<ScoreType> scores{ 1,2 };
    Individual pickedParent{ std::vector<bool>({ true, false }) };
    Individual crossedIndividual{ std::vector<bool>({ false, true }) };
    Individual mutatedIndividual{ std::vector<bool>({ true, true }) };

    void SetUp() override
    {
        generation = Generation(std::vector<Individual>(generationData));
    }

    IndividualsBuilderStrategy getBuilder() const
    {
        ::Tests::MockCrossoverOperator crossover;
        ::Tests::MockMutationOperator mutation;
        ::Tests::MockParentSelectionStrategy parentSelectionStrategy;

        EXPECT_CALL(parentSelectionStrategy, next(_, _)).WillRepeatedly(Return(Individual(pickedParent)));
        EXPECT_CALL(crossover, CallOperator(_, _)).WillRepeatedly(Return(Individual(crossedIndividual)));
        EXPECT_CALL(mutation, CallOperator(_)).WillRepeatedly(Return(mutatedIndividual));

        IndividualsBuilderStrategy strategy(std::move(crossover), std::move(mutation), std::move(parentSelectionStrategy));
        return strategy;
    }
};

TEST_F(IndividualsBuilderTest, initializes)
{
    ::Tests::MockCrossoverOperator crossover;
    ::Tests::MockMutationOperator mutation;
    ::Tests::MockParentSelectionStrategy parentSelectionStrategy;
    EXPECT_NO_THROW(IndividualsBuilderStrategy(std::move(crossover), std::move(mutation), std::move(parentSelectionStrategy)));
}

TEST_F(IndividualsBuilderTest, throws_for_inconsistent_generation_and_scores_size)
{
    auto strategy = getBuilder();
    EXPECT_THROW(strategy.Build(generation, std::vector<ScoreType>(generation.size() - 1, 1), 1), InconsistentGenerationAndScoresLengthException);
}

TEST_F(IndividualsBuilderTest, builds_population_of_specified_size)
{
    auto strategy = getBuilder();
    const auto newSize = 5;
    auto newGeneration = strategy.Build(generation, scores, newSize);
    EXPECT_EQ(newGeneration.size(), newSize);
}

TEST_F(IndividualsBuilderTest, returns_individuals_after_mutation)
{
    auto strategy = getBuilder();
    const auto newSize = 1;
    auto newGeneration = strategy.Build(generation, scores, newSize);
    EXPECT_EQ(newGeneration[0], mutatedIndividual);
}
}
