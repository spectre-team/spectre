/*
* MutationOperatorTest.cpp
* Tests mutation of an individual.
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
#include "Spectre.libException/ArgumentOutOfRangeException.h"
#include "Spectre.libGenetic/MutationOperator.h"

namespace
{
using namespace Spectre::libGenetic;
using namespace Spectre::libException;

TEST(MutationTestInitialization, initializes_for_valid_data)
{
    EXPECT_NO_THROW(MutationOperator(0, 0, 0));
}

TEST(MutationTestInitialization, throws_for_negative_mutation_rate)
{
    EXPECT_THROW(MutationOperator(-1, 0, 0), ArgumentOutOfRangeException<double>);
}

TEST(MutationTestInitialization, throws_for_excessive_mutation_rate)
{
    EXPECT_THROW(MutationOperator(1.1, 0, 0), ArgumentOutOfRangeException<double>);
}

TEST(MutationTestInitialization, throws_for_negative_bit_swap_rate)
{
    EXPECT_THROW(MutationOperator(0, -1, 0), ArgumentOutOfRangeException<double>);
}

TEST(MutationTestInitialization, throws_for_excessive_bit_swap_rate)
{
    EXPECT_THROW(MutationOperator(0, 1.1, 0), ArgumentOutOfRangeException<double>);
}

class MutationTest: public ::testing::Test
{
public:
    MutationTest():
        individual(std::vector<bool>(defaultData)) {}

protected:
    const unsigned NUMBER_OF_TRIALS = 1000;
    const double ALLOWED_MISS_RATE = 0.05;
    const double ALWAYS = 1;
    const double NEVER = 0;
    const Seed SEED = 0;
    const std::vector<bool> defaultData { false, false, false };
    Individual individual;

    void SetUp() override
    {
        individual = Individual(std::vector<bool>(defaultData));
    }
};

TEST_F(MutationTest, mutated_has_the_same_size)
{
    MutationOperator mutate(ALWAYS, ALWAYS, SEED);
    const auto size = individual.size();
    const auto mutant = mutate(std::move(individual));
    EXPECT_EQ(mutant.size(), size);
}

TEST_F(MutationTest, seed_provides_repeatibility)
{
    MutationOperator first(0.5, 0.5, SEED);
    MutationOperator second(0.5, 0.5, SEED);

    for (auto i = 0u; i < NUMBER_OF_TRIALS; ++i)
    {
        Individual firstCopy { std::vector<bool>(defaultData) };
        Individual secondCopy { std::vector<bool>(defaultData) };

        const auto firstMutant = first(std::move(firstCopy));
        const auto secondMutant = second(std::move(secondCopy));

        for (size_t j = 0; j < firstMutant.size(); ++j)
        {
            EXPECT_EQ(firstMutant[j], secondMutant[j]);
        }
    }
}

TEST_F(MutationTest, changes_nothing_when_zero_mutation_rate)
{
    MutationOperator mutate(NEVER, ALWAYS, SEED);
    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        individual = mutate(std::move(individual));
        for (size_t j = 0; j < individual.size(); ++j)
        {
            EXPECT_FALSE(individual[j]) << "trial: " << i << "; bit: " << j;
        }
    }
}

TEST_F(MutationTest, changes_nothing_when_zero_bit_swap_rate)
{
    MutationOperator mutate(ALWAYS, NEVER, SEED);
    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        individual = mutate(std::move(individual));
        for (size_t j = 0; j < individual.size(); ++j)
        {
            EXPECT_FALSE(individual[j]) << "trial: " << i << "; bit: " << j;
        }
    }
}

TEST_F(MutationTest, swaps_all_bits_on_both_rates_equal_one)
{
    MutationOperator mutate(ALWAYS, ALWAYS, SEED);
    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        const auto last = Individual(std::vector<bool>(individual.begin(), individual.end()));
        individual = mutate(std::move(individual));
        for (size_t j = 0; j < individual.size(); ++j)
        {
            EXPECT_NE(individual[j], last[j]) << "trial: " << i << "; bit: " << j;
        }
    }
}

TEST_F(MutationTest, toggles_in_approximate_percentage_of_cases_for_specified_mutation_rate)
{
    const auto MUTATION_RATE = 0.5;
    MutationOperator mutate(MUTATION_RATE, ALWAYS, SEED);
    const auto expectedNumberOfToggles = NUMBER_OF_TRIALS * individual.size() * MUTATION_RATE;
    const auto allowedMissCount = ALLOWED_MISS_RATE * expectedNumberOfToggles;
    unsigned numberOfToggles = 0;
    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        const auto last = Individual(std::vector<bool>(individual.begin(), individual.end()));
        individual = mutate(std::move(individual));
        for (size_t j = 0; j < individual.size(); ++j)
        {
            numberOfToggles += last[j] != individual[j];
        }
    }
    EXPECT_LT(numberOfToggles, expectedNumberOfToggles + allowedMissCount);
    EXPECT_GT(numberOfToggles, expectedNumberOfToggles - allowedMissCount);
}

TEST_F(MutationTest, toggles_in_approximate_percentage_of_cases_for_specified_bit_swap_rate)
{
    const auto BIT_SWAP_RATE = 0.5;
    MutationOperator mutate(ALWAYS, BIT_SWAP_RATE, SEED);
    const auto expectedNumberOfToggles = NUMBER_OF_TRIALS * individual.size() * BIT_SWAP_RATE;
    const auto allowedMissCount = ALLOWED_MISS_RATE * expectedNumberOfToggles;
    unsigned numberOfToggles = 0;
    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        const auto last = Individual(std::vector<bool>(individual.begin(), individual.end()));
        individual = mutate(std::move(individual));
        for (size_t j = 0; j < individual.size(); ++j)
        {
            numberOfToggles += last[j] != individual[j];
        }
    }
    EXPECT_LT(numberOfToggles, expectedNumberOfToggles + allowedMissCount);
    EXPECT_GT(numberOfToggles, expectedNumberOfToggles - allowedMissCount);
}

TEST_F(MutationTest, test_fillup_returns_original)
{
    const auto BIT_SWAP_RATE = 0.5;
    MutationOperator mutate(ALWAYS, BIT_SWAP_RATE, SEED, 8, 9);
    for (unsigned i = 0; i < NUMBER_OF_TRIALS; ++i)
    {
        const auto last = Individual(std::vector<bool>(individual.begin(), individual.end()));
        individual = mutate(std::move(individual));
        for (auto j = 0; j < individual.size(); j++)
        {
            EXPECT_EQ(individual[j], last[j]);
        }
    }
}
}
