/*
* ParentSelectionStrategyTest.cpp
* Tests generation.
*
Copyright 2017 Grzegorz Mrukwa, Wojciech Wilgierz

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
#include <numeric>
#include "Spectre.libException/ArgumentOutOfRangeException.h"
#include "Spectre.libGenetic/ParentSelectionStrategy.h"
#include "Spectre.libGenetic/InconsistentGenerationAndScoresLengthException.h"

namespace
{
using namespace Spectre::libGenetic;
using namespace Spectre::libException;

TEST(ParentSelectionStrategyInitialization, initializes)
{
    ParentSelectionStrategy parent_selection(0);
}

class ParentSelectionStrategyTest : public ::testing::Test
{
public:
    ParentSelectionStrategyTest():
        individual1(std::vector<bool> { true, false, true, false }),
        individual2(std::vector<bool> { false, true, false, true }),
        generation({ individual1, individual2 }) {}

protected:
    const unsigned NUMBER_OF_TRIALS = 1000;
    const double ALLOWED_MISS_RATE = 0.05;
    const Seed SEED = 0;
    const Individual individual1;
    const Individual individual2;
    ParentSelectionStrategy parent_selection;
    Generation generation;

    void SetUp() override
    {
        parent_selection = ParentSelectionStrategy(SEED);
    }
};

TEST_F(ParentSelectionStrategyTest, all_zero_scores_do_not_cause_error)
{
    const std::vector<ScoreType> score { 0, 0 };

    EXPECT_NO_THROW(parent_selection.next(generation, score));
}

TEST_F(ParentSelectionStrategyTest, throws_for_negative_weights)
{
    const std::vector<ScoreType> score { -1, 0 };

    EXPECT_THROW(parent_selection.next(generation, score), ArgumentOutOfRangeException<ScoreType>);
}

TEST_F(ParentSelectionStrategyTest, throws_on_inconsistent_inputs_size)
{
    std::vector<ScoreType> tooShortScores({ 0 });
    EXPECT_THROW(parent_selection.next(generation, tooShortScores), InconsistentGenerationAndScoresLengthException);
}

TEST_F(ParentSelectionStrategyTest, some_zero_scores_never_draw_corresponding_individuals)
{
    const std::vector<ScoreType> score { 1, 0 };

    for (auto i = 0u; i < NUMBER_OF_TRIALS; ++i)
    {
        const auto parents = parent_selection.next(generation, score);
        EXPECT_NE(individual2, parents.first);
        EXPECT_NE(individual2, parents.second);
    }
}

TEST_F(ParentSelectionStrategyTest, scores_influence_draw_probability_proportionally)
{
    const std::vector<ScoreType> score { 1, 2 };

    auto count1 = 0u;
    auto count2 = 0u;

    for (auto i = 0u; i < NUMBER_OF_TRIALS; ++i)
    {
        const auto parents = parent_selection.next(generation, score);
        const auto &firstParent = parents.first.get();
        const auto &secondParent = parents.second.get();
        if (firstParent == generation[0])
        {
            ++count1;
        }
        else
        {
            ++count2;
        }
        if (secondParent == generation[0])
        {
            ++count1;
        }
        else
        {
            ++count2;
        }
    }

    const auto expectedCount1 = score[0] * NUMBER_OF_TRIALS * 2. / (score[0] + score[1]);
    const auto expectedCount2 = score[1] * NUMBER_OF_TRIALS * 2. / (score[0] + score[1]);

    EXPECT_GT(count1, expectedCount1 - NUMBER_OF_TRIALS * ALLOWED_MISS_RATE);
    EXPECT_LT(count1, expectedCount1 + NUMBER_OF_TRIALS * ALLOWED_MISS_RATE);
    EXPECT_GT(count2, expectedCount2 - NUMBER_OF_TRIALS * ALLOWED_MISS_RATE);
    EXPECT_LT(count2, expectedCount2 + NUMBER_OF_TRIALS * ALLOWED_MISS_RATE);
}
}
