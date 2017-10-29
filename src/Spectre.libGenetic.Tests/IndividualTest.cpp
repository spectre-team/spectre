/*
* IndividualTest.cpp
* Tests individual.
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
#include "Spectre.libException/OutOfRangeException.h"
#include "Spectre.libGenetic/Individual.h"
#include "Spectre.libGenetic/InconsistentChromosomeLengthException.h"

namespace
{
using namespace Spectre::libGenetic;

const auto seed = 0ul;

TEST(IndividualInitialization, initializes)
{
    EXPECT_NO_THROW(Individual({ true, true, true, true }));
    EXPECT_NO_THROW(Individual({ false, false, false, false }));
    EXPECT_NO_THROW(Individual({ true, false, true, false }));
}

TEST(IndividualInitialization, initializes_by_shuffle)
{
    const auto individualSize = 6u;
    const auto initialFillup = 4u;
    const auto excessiveFillup = 8u;
    EXPECT_NO_THROW(Individual(individualSize, initialFillup, seed));
    EXPECT_THROW(Individual(individualSize, excessiveFillup, seed), Spectre::libException::ArgumentOutOfRangeException<size_t>);
}

class IndividualTest : public ::testing::Test
{
public:
    IndividualTest():
        trueIndividual(std::vector<bool>(TRUE_DATA)),
        falseIndividual(std::vector<bool>(FALSE_DATA)),
        mixedIndividual(std::vector<bool>(MIXED_DATA)) {}

protected:
    const std::vector<bool> TRUE_DATA { true, true, true, true };
    const std::vector<bool> FALSE_DATA { false, false, false, false };
    const std::vector<bool> MIXED_DATA { true, false, true, false };
    const Individual trueIndividual;
    const Individual falseIndividual;
    const Individual mixedIndividual;
};

TEST_F(IndividualTest, assignment_throws_on_inconsistent_chromosome_length)
{
    Individual individual(std::vector<bool> { true, false, true });
    EXPECT_THROW(individual = Individual(std::vector<bool>{true, false}), InconsistentChromosomeLengthException);
}

TEST_F(IndividualTest, exhibit_proper_size)
{
    const auto size = trueIndividual.size();
    EXPECT_EQ(size, TRUE_DATA.size());
}

TEST_F(IndividualTest, const_index_throws_for_exceeded_size)
{
    EXPECT_THROW(mixedIndividual[mixedIndividual.size()], Spectre::libException::OutOfRangeException);
}

TEST_F(IndividualTest, mutable_index_throws_for_exceeded_size)
{
    Individual individual { std::vector<bool>(MIXED_DATA) };
    EXPECT_THROW(individual[individual.size()] = false, Spectre::libException::OutOfRangeException);
}

TEST_F(IndividualTest, index_returns_proper_const_bits)
{
    for (auto i = 0u; i < MIXED_DATA.size(); ++i)
    {
        EXPECT_EQ(mixedIndividual[i], MIXED_DATA[i]);
    }
}

TEST_F(IndividualTest, index_allows_modification)
{
    Individual individual({ false, true });

    individual[0] = true;

    EXPECT_TRUE(individual[0]);
}

TEST_F(IndividualTest, iterators_allow_to_iterate_over_const_binary_data)
{
    auto individualIterator = mixedIndividual.begin();
    auto dataIterator = MIXED_DATA.begin();

    while (individualIterator != mixedIndividual.end() && dataIterator != MIXED_DATA.end())
    {
        EXPECT_EQ(*individualIterator, *dataIterator);
        ++individualIterator;
        ++dataIterator;
    }

    EXPECT_EQ(individualIterator, mixedIndividual.end());
    EXPECT_EQ(dataIterator, MIXED_DATA.end());
}

TEST_F(IndividualTest, iterators_allow_to_read_and_modify_binary_data)
{
    Individual mutableIndividual { std::vector<bool>(MIXED_DATA) };
    auto individualIterator = mutableIndividual.begin();
    auto dataIterator = MIXED_DATA.begin();

    while (individualIterator != mutableIndividual.end() && dataIterator != MIXED_DATA.end())
    {
        EXPECT_EQ(*individualIterator, *dataIterator);
        *individualIterator = !*individualIterator;
        ++individualIterator;
        ++dataIterator;
    }

    EXPECT_EQ(individualIterator, mutableIndividual.end());
    EXPECT_EQ(dataIterator, MIXED_DATA.end());

    individualIterator = mutableIndividual.begin();
    dataIterator = MIXED_DATA.begin();
    while (individualIterator != mutableIndividual.end() && dataIterator != MIXED_DATA.end())
    {
        EXPECT_EQ(*individualIterator, !*dataIterator);
        ++individualIterator;
        ++dataIterator;
    }
}

TEST_F(IndividualTest, equality_different_individuals_marked_unequal)
{
    EXPECT_FALSE(trueIndividual == falseIndividual);
}

TEST_F(IndividualTest, equality_same_individuals_marked_equal)
{
    const Individual copy { std::vector<bool>(MIXED_DATA) };
    EXPECT_TRUE(copy == mixedIndividual);
}

TEST_F(IndividualTest, unequality_different_individuals_marked_unequal)
{
    EXPECT_TRUE(trueIndividual != falseIndividual);
}

TEST_F(IndividualTest, unequality_same_individuals_marked_equal)
{
    const Individual copy { std::vector<bool>(MIXED_DATA) };
    EXPECT_FALSE(copy != mixedIndividual);
}

TEST_F(IndividualTest, true_amount_equal_to_parameter)
{
    const auto individualSize = 10u;
    const auto initialFillup = 6u;
    Individual individual(individualSize, initialFillup, seed);
    auto trueAmount = 0u;
    for (auto i = 0; i < individual.size(); i++)
    {
        if (individual[i] == true)
        {
            trueAmount++;
        }
    }
    EXPECT_EQ(trueAmount, initialFillup);
}
}
