/*
* PreservationStrategyTest.cpp
* Tests PreservationStrategy.
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
#include "Spectre.libGenetic/PreservationStrategy.h"
#include "Spectre.libException/ArgumentOutOfRangeException.h"
#include "Spectre.libGenetic/InconsistentGenerationAndScoresLengthException.h"

namespace
{
using namespace Spectre::libGenetic;

class PreservationStrategyTest: public ::testing::Test
{
public:
    PreservationStrategyTest():
        individual1({true}),
        individual2({false}),
        generation({individual1, individual2}),
        scores({1,2}),
        fakeBigGeneration({}),
        fakeBigGenerationScores({})
    {}
protected:
    const Individual individual1;
    const Individual individual2;
    const Generation generation;
    const std::vector<ScoreType> scores;
    Generation fakeBigGeneration;
    std::vector<ScoreType> fakeBigGenerationScores;

    void SetUp() override
    {
        fakeBigGeneration = generation + generation;
        for (auto i = 0u; i < 8; ++i)
            fakeBigGeneration = fakeBigGeneration + fakeBigGeneration;
        fakeBigGenerationScores.reserve(fakeBigGeneration.size());
        for (auto i = 0u; i < fakeBigGeneration.size(); ++i)
            fakeBigGenerationScores.push_back(i % 2);
    }
};

TEST_F(PreservationStrategyTest, initializes)
{
	EXPECT_NO_THROW(PreservationStrategy(0.5));
}

TEST_F(PreservationStrategyTest, throws_for_negative_preservation_rate)
{
    EXPECT_THROW(PreservationStrategy(-0.1), Spectre::libException::ArgumentOutOfRangeException<double>);
}

TEST_F(PreservationStrategyTest, throws_for_excessive_preservation_rate)
{
    EXPECT_THROW(PreservationStrategy(1.1), Spectre::libException::ArgumentOutOfRangeException<double>);
}

TEST_F(PreservationStrategyTest, throws_for_mismatching_generation_and_scores_size)
{
    PreservationStrategy preservation(0);

    const std::vector<ScoreType> tooShortScore{ 1 };

    EXPECT_THROW(preservation.PickBest(generation, tooShortScore), InconsistentGenerationAndScoresLengthException);
}

TEST_F(PreservationStrategyTest, picks_none_with_zero_preservation_rate)
{
	PreservationStrategy preservation(0.0);

    const auto newGeneration = preservation.PickBest(generation, scores);

	EXPECT_EQ(newGeneration.size(), 0);
}

TEST_F(PreservationStrategyTest, picks_all_with_one_preservation_rate)
{
    PreservationStrategy preservation(1);

    const auto newGeneration = preservation.PickBest(generation, scores);

    EXPECT_EQ(newGeneration.size(), generation.size());
}

TEST_F(PreservationStrategyTest, picks_around_preservation_rate)
{
    PreservationStrategy preservation(.5);

    const auto newGeneration = preservation.PickBest(fakeBigGeneration, fakeBigGenerationScores);

    EXPECT_EQ(newGeneration.size(), fakeBigGeneration.size() / 2);
}

TEST_F(PreservationStrategyTest, picks_best)
{
    PreservationStrategy preservation(.5);

    const auto newGeneration = preservation.PickBest(generation, scores);

    ASSERT_EQ(newGeneration.size(), 1);

    const auto first = *newGeneration.begin();

    EXPECT_EQ(first, individual2);
}

TEST_F(PreservationStrategyTest, new_generation_is_sorted_decreasing_by_score)
{
    PreservationStrategy preservation(1);

    const auto newGeneration = preservation.PickBest(generation, scores);

    ASSERT_EQ(newGeneration.size(), generation.size());

    const auto first = *newGeneration.begin();
    const auto second = *(newGeneration.begin()+1);

    EXPECT_EQ(first, individual2);
    EXPECT_EQ(second, individual1);
}
}
