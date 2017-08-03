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
#include "Spectre.libException/NullPointerException.h"
#include "Spectre.libGenetic/IndividualsBuilderStrategy.h"
#include "Spectre.libGenetic/InconsistentGenerationAndScoresLengthException.h"
#include "MockCrossoverOperator.h"
#include "MockMutationOperator.h"
#include "MockParentSelectionStrategy.h"

namespace
{
using namespace Spectre::libGenetic;
using namespace ::testing;

reference_pair<Individual> toReferencePair(std::initializer_list<bool> binaryData)
{
    Individual individual(std::move(binaryData));
    std::reference_wrapper<Individual> referenceWrapper(individual);
    return std::make_pair(referenceWrapper, referenceWrapper);
}

class IndividualsBuilderTest: public ::testing::Test
{
public:
    IndividualsBuilderTest():
        generation(std::vector<Individual>(generationData)) {}

protected:
    const std::vector<Individual> generationData { Individual({ true, true }), Individual({ false, true }) };
    Generation generation;
    const std::vector<ScoreType> scores { 1,2 };
    reference_pair<Individual> pickedParents { toReferencePair({ true, false }) };
    Individual crossedIndividual { std::vector<bool>({ false, true }) };
    Individual mutatedIndividual { std::vector<bool>({ true, true }) };

    void SetUp() override
    {
        generation = Generation(std::vector<Individual>(generationData));
    }

    std::unique_ptr<IndividualsBuilderStrategy> getBuilder(size_t newSize) const
    {
        auto crossover = std::make_unique<::Tests::MockCrossoverOperator>();
        auto mutation = std::make_unique<::Tests::MockMutationOperator>();
        auto parentSelectionStrategy = std::make_unique<::Tests::MockParentSelectionStrategy>();

        auto size = static_cast<int>(newSize);

        if (size != 0)
        {
            EXPECT_CALL(*parentSelectionStrategy, next(_, _)).Times(size).WillRepeatedly(Return(pickedParents));
            EXPECT_CALL(*crossover, CallOperator(_, _)).Times(size).WillRepeatedly(Return(Individual(crossedIndividual)));
            EXPECT_CALL(*mutation, CallOperator(_)).Times(size).WillRepeatedly(Return(mutatedIndividual));
        }
        else
        {
            EXPECT_CALL(*parentSelectionStrategy, next(_, _)).Times(size);
            EXPECT_CALL(*crossover, CallOperator(_, _)).Times(size);
            EXPECT_CALL(*mutation, CallOperator(_)).Times(size);
        }

        auto strategy = std::make_unique<IndividualsBuilderStrategy>(std::move(crossover), std::move(mutation), std::move(parentSelectionStrategy));
        return std::move(strategy);
    }
};

TEST_F(IndividualsBuilderTest, initializes)
{
    auto crossover = std::make_unique<::Tests::MockCrossoverOperator>();
    auto mutation = std::make_unique<::Tests::MockMutationOperator>();
    auto parentSelectionStrategy = std::make_unique<::Tests::MockParentSelectionStrategy>();
    EXPECT_NO_THROW(IndividualsBuilderStrategy(std::move(crossover), std::move(mutation), std::move(parentSelectionStrategy)));
}

TEST_F(IndividualsBuilderTest, initialization_throws_for_nullptr_crossover)
{
    auto crossover = nullptr;
    auto mutation = std::make_unique<::Tests::MockMutationOperator>();
    auto parentSelectionStrategy = std::make_unique<::Tests::MockParentSelectionStrategy>();
    EXPECT_THROW(IndividualsBuilderStrategy(std::move(crossover), std::move(mutation), std::move(parentSelectionStrategy)), Spectre::libException::NullPointerException);
}

TEST_F(IndividualsBuilderTest, initialization_throws_for_nullptr_mutation)
{
    auto crossover = std::make_unique<::Tests::MockCrossoverOperator>();
    auto mutation = nullptr;
    auto parentSelectionStrategy = std::make_unique<::Tests::MockParentSelectionStrategy>();
    EXPECT_THROW(IndividualsBuilderStrategy(std::move(crossover), std::move(mutation), std::move(parentSelectionStrategy)), Spectre::libException::NullPointerException);
}

TEST_F(IndividualsBuilderTest, initialization_throws_for_nullptr_parent_selection)
{
    auto crossover = std::make_unique<::Tests::MockCrossoverOperator>();
    auto mutation = std::make_unique<::Tests::MockMutationOperator>();
    auto parentSelectionStrategy = nullptr;
    EXPECT_THROW(IndividualsBuilderStrategy(std::move(crossover), std::move(mutation), std::move(parentSelectionStrategy)), Spectre::libException::NullPointerException);
}

TEST_F(IndividualsBuilderTest, throws_for_inconsistent_generation_and_scores_size)
{
    const auto newSize = 0;
    auto strategy = getBuilder(newSize);
    std::vector<ScoreType> tooShortScores(generation.size() - 1, 1);
    EXPECT_THROW(strategy->Build(generation, tooShortScores, newSize), InconsistentGenerationAndScoresLengthException);
}

TEST_F(IndividualsBuilderTest, builds_population_of_specified_size)
{
    const auto newSize = 5;
    auto strategy = getBuilder(newSize);
    auto newGeneration = strategy->Build(generation, scores, newSize);
    EXPECT_EQ(newGeneration.size(), newSize);
}

TEST_F(IndividualsBuilderTest, returns_individuals_after_mutation)
{
    const auto newSize = 1;
    auto strategy = getBuilder(newSize);
    auto newGeneration = strategy->Build(generation, scores, newSize);
    EXPECT_EQ(newGeneration[0], mutatedIndividual);
}
}
