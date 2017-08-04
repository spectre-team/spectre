/*
* OffspringGeneratorTest.cpp
* Tests offspring generator.
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
#include "Spectre.libGenetic/InconsistentGenerationAndScoresLengthException.h"
#include "Spectre.libGenetic/OffspringGenerator.h"
#include "MockIndividualsBuilderStrategy.h"
#include "MockPreservationStrategy.h"

namespace
{
using namespace Spectre::libGenetic;
using namespace Spectre::libException;
using namespace ::testing;

class OffspringGeneratorTest : public Test
{
public:
    OffspringGeneratorTest() {}
protected:
    const Individual preservedIndividual { std::vector<bool> { true, true, true, true } };
    const Individual generatedIndividual { std::vector<bool> { false, false, false, false } };
    const Individual oldIndividual { std::vector<bool> { false, true, false, true } };
    const Generation preserved { std::vector<Individual> { preservedIndividual } };
    const Generation generated { std::vector<Individual> { generatedIndividual, generatedIndividual, generatedIndividual } };
    Generation old { std::vector<Individual> { oldIndividual, oldIndividual, oldIndividual, oldIndividual } };
    const std::vector<ScoreType> scores { 1, 2, 3, 4 };
    std::unique_ptr<Tests::MockIndividualsBuilderStrategy> builder;
    std::unique_ptr<Tests::MockPreservationStrategy> preservationStrategy;

    void SetUp() override
    {
        builder = std::make_unique<Tests::MockIndividualsBuilderStrategy>();
        preservationStrategy = std::make_unique<Tests::MockPreservationStrategy>();
    }
};

TEST_F(OffspringGeneratorTest, initializes)
{
    EXPECT_NO_THROW(OffspringGenerator(std::move(builder), std::move(preservationStrategy)));
}

TEST_F(OffspringGeneratorTest, initialization_throws_for_nullptr_builder)
{
    builder = nullptr;
    EXPECT_THROW(OffspringGenerator(std::move(builder), std::move(preservationStrategy)), NullPointerException);
}

TEST_F(OffspringGeneratorTest, initialization_throws_for_nullptr_preservation_strategy)
{
    preservationStrategy = nullptr;
    EXPECT_THROW(OffspringGenerator(std::move(builder), std::move(preservationStrategy)), NullPointerException);
}

TEST_F(OffspringGeneratorTest, new_generation_has_proper_size)
{
    EXPECT_CALL(*preservationStrategy, PickBest(_, _)).WillOnce(Return(preserved));
    EXPECT_CALL(*builder, BuildFunction(_, _, _)).WillOnce(Return(generated));
    OffspringGenerator generator(std::move(builder), std::move(preservationStrategy));
    const auto newGeneration = generator.next(old, scores);
    EXPECT_EQ(newGeneration.size(), old.size());
}

TEST_F(OffspringGeneratorTest, new_generation_preserves_specified_part_of_old)
{
    EXPECT_CALL(*preservationStrategy, PickBest(_, _)).WillOnce(Return(preserved));
    EXPECT_CALL(*builder, BuildFunction(_, _, _)).WillOnce(Return(generated));
    OffspringGenerator generator(std::move(builder), std::move(preservationStrategy));
    const auto newGeneration = generator.next(old, scores);
    auto preservedCount = 0u;
    for (auto &individual: newGeneration)
    {
        if (individual == preservedIndividual)
        {
            ++preservedCount;
        }
    }
    EXPECT_EQ(preservedCount, preserved.size());
}

TEST_F(OffspringGeneratorTest, new_generation_creates_remaining_non_preserved)
{
    EXPECT_CALL(*preservationStrategy, PickBest(_, _)).WillOnce(Return(preserved));
    EXPECT_CALL(*builder, BuildFunction(_, _, _)).WillOnce(Return(generated));
    OffspringGenerator generator(std::move(builder), std::move(preservationStrategy));
    const auto newGeneration = generator.next(old, scores);
    auto newCount = 0u;
    for (auto &individual : newGeneration)
    {
        if (individual != preservedIndividual)
        {
            ++newCount;
        }
    }
    EXPECT_EQ(newCount, generated.size());
}

TEST_F(OffspringGeneratorTest, throws_on_inconsistent_sizes)
{
    EXPECT_CALL(*preservationStrategy, PickBest(_, _)).Times(0);
    EXPECT_CALL(*builder, BuildFunction(_, _, _)).Times(0);
    OffspringGenerator generator(std::move(builder), std::move(preservationStrategy));
    std::vector<ScoreType> tooShortScores(scores.begin(), scores.end() - 1);
    EXPECT_THROW(generator.next(old, tooShortScores), InconsistentGenerationAndScoresLengthException);
}
}
