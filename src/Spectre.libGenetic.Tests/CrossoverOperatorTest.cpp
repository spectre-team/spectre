/*
* CrossoverOperatorTest.cpp
* Tests crossover operator.
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
#include <numeric>
#include <memory>
#include "Spectre.libGenetic/CrossoverOperator.h"
#include "Spectre.libGenetic/InconsistentChromosomeLengthException.h"

namespace
{
using namespace Spectre::libGenetic;

TEST(CrossoverOperatorInitialization, initializes)
{
    CrossoverOperator crossover(0);
}

class CrossoverOperatorTest: public ::testing::Test
{
public:
    CrossoverOperatorTest() {}
protected:
    const unsigned NUMBER_OF_TRIALS = 1000;
    const double ALLOWED_MISS_RATE = 0.05;
    const Seed SEED = 0;
    const Individual true_individual = Individual({ true, true, true, true });
    const Individual false_individual = Individual({ false, false, false, false });
    std::unique_ptr<CrossoverOperator> crossover;

    void SetUp() override
    {
        crossover = std::make_unique<CrossoverOperator>(SEED);
    }
};

TEST_F(CrossoverOperatorTest, throws_on_inconsistent_sizes)
{
    const Individual shorter({ false });
    const Individual longer({ false, true });
    EXPECT_THROW(crossover->operator()(shorter, longer), InconsistentChromosomeLengthException);
}

TEST_F(CrossoverOperatorTest, child_has_the_same_size)
{
    const auto parentsSize = true_individual.size();
    const auto child = crossover->operator()(true_individual, false_individual);
    EXPECT_EQ(child.size(), parentsSize);
}

TEST_F(CrossoverOperatorTest, crossover_of_same_parents_result_in_copy)
{
    const auto singleParent = true_individual;
    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        const auto child = crossover->operator()(singleParent, singleParent);
        for (size_t j = 0; j < child.size(); ++j)
        {
            EXPECT_EQ(child[j], singleParent[j]);
        }
    }
}

TEST_F(CrossoverOperatorTest, cuts_with_symmetric_distribution)
{
    const auto expectedCounts = NUMBER_OF_TRIALS * true_individual.size() * 0.5;
    const auto allowedCountsMiss = expectedCounts * ALLOWED_MISS_RATE;
    unsigned counts = 0;
    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        const auto child = crossover->operator()(true_individual, false_individual);
        counts = std::accumulate(child.begin(), child.end(), counts);
    }
    EXPECT_GT(counts, expectedCounts - allowedCountsMiss);
    EXPECT_LT(counts, expectedCounts + allowedCountsMiss);
}

TEST_F(CrossoverOperatorTest, seed_provides_repeatibility)
{
    CrossoverOperator first(SEED);
    CrossoverOperator second(SEED);

    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        const auto firstChild = first(true_individual, false_individual);
        const auto secondChild = second(true_individual, false_individual);

        for (size_t j = 0; j < firstChild.size(); ++j)
        {
            EXPECT_EQ(firstChild[j], secondChild[j]);
        }
    }
}

TEST_F(CrossoverOperatorTest, child_is_constructed_from_parents_in_order)
{
    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        CrossoverOperator first(i);
        const auto firstChild = first(true_individual, false_individual);

        CrossoverOperator second(i);
        const auto secondChild = second(false_individual, true_individual);

        for (size_t j = 0; j < firstChild.size(); ++j)
        {
            EXPECT_NE(firstChild[j], secondChild[j]);
        }
    }
}

TEST_F(CrossoverOperatorTest, child_is_constructed_with_single_cut)
{
    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        const auto child = crossover->operator()(true_individual, false_individual);
        const unsigned bitCounts = std::accumulate(child.begin(), child.end(), 0);
        const unsigned frontBitCounts = std::accumulate(child.begin(), child.begin() + bitCounts, 0);
        EXPECT_EQ(bitCounts, frontBitCounts);
    }
}

TEST_F(CrossoverOperatorTest, cuts_are_from_uniform_distribution)
{
    std::vector<unsigned> counts(true_individual.size() + 1, 0);
    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        const auto child = crossover->operator()(true_individual, false_individual);
        const unsigned bitCounts = std::accumulate(child.begin(), child.end(), 0);
        ++counts[bitCounts];
    }
    const auto meanCount = NUMBER_OF_TRIALS / (true_individual.size() + 1);
    const auto allowedCountMiss = meanCount * ALLOWED_MISS_RATE;
    for (unsigned i = 0; i < counts.size(); ++i)
    {
        const auto count = counts[i];
        EXPECT_GT(count, meanCount - allowedCountMiss) << i;
        EXPECT_LT(count, meanCount + allowedCountMiss) << i;
    }
}
}
