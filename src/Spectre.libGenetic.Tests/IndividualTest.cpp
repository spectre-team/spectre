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

#define GTEST_LANG_CXX11 1

#include <gtest/gtest.h>
#include "Spectre.libGenetic/Individual.h"

namespace
{
using namespace Spectre::libGenetic;

TEST(IndividualInitialization, initializes)
{
	EXPECT_NO_THROW(Individual({ true, true, true, true }));
    EXPECT_NO_THROW(Individual({ false, false, false, false }));
    EXPECT_NO_THROW(Individual({ true, false, true, false }));
}

class IndividualTest : public ::testing::Test
{
public:
	IndividualTest():
        trueIndividual(std::vector<bool>(TRUE_DATA)),
        falseIndividual(std::vector<bool>(FALSE_DATA)),
        mixedIndividual(std::vector<bool>(MIXED_DATA))
    {}
protected:
    const std::vector<bool> TRUE_DATA{ true, true, true, true };
    const std::vector<bool> FALSE_DATA{ false, false, false, false };
    const std::vector<bool> MIXED_DATA{ true, false, true, false };
    const Individual trueIndividual;
    const Individual falseIndividual;
    const Individual mixedIndividual;
};

TEST_F(IndividualTest, exhibit_proper_size)
{
	const auto size = trueIndividual.size();
	EXPECT_EQ(size, 4);
}

TEST_F(IndividualTest, index_returns_proper_const_bits)
{
	EXPECT_EQ(mixedIndividual[0], true);
	EXPECT_EQ(mixedIndividual[1], false);
	EXPECT_EQ(mixedIndividual[2], true);
	EXPECT_EQ(mixedIndividual[3], false);
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
    Individual mutableIndividual{std::vector<bool>(MIXED_DATA)};
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
    }
}

TEST_F(IndividualTest, different_individuals_marked_unequal)
{
    EXPECT_FALSE(trueIndividual == falseIndividual);
}

TEST_F(IndividualTest, same_individuals_marked_equal)
{
    const Individual copy{ std::vector<bool>(MIXED_DATA) };
    EXPECT_TRUE(copy == mixedIndividual);
}
}
