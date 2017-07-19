/*
* MutationOperatorTest.cpp
* Tests mutation of an individual.
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

#define GTEST_LANG_CXX11 1

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
}
