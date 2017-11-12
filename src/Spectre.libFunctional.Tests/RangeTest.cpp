/*
* RangeTest.cpp
* Tests range-related functions.
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
#include <gmock/gmock-matchers.h>
#include "Spectre.libFunctional/Range.h"

namespace
{
using namespace testing;
using namespace Spectre::libPlatform::Functional;

TEST(RangeTest, throws_for_step_zero)
{
    EXPECT_THROW(range(0, 0, 0), ZeroStepException);
}

TEST(RangeTest, is_empty_for_upper_bound_equal_lower)
{
    const auto numbers = range(0, 0, 1);
    EXPECT_EQ(numbers.size(), 0);
}

TEST(RangeTest, last_item_plus_step_is_upper_bound_for_million_floats_and_matching_step)
{
    const auto lowerBound = 0.0f;
    const auto upperBound = 100.0f;
    const auto numberOfElements = 1000000u;
    const auto step = (upperBound - lowerBound) / numberOfElements;
    const auto floats = range(lowerBound, upperBound, step);
    EXPECT_THAT(floats.back() + step, FloatEq(upperBound));
}

TEST(RangeTest, proceeds_by_step)
{
    const auto lowerBound = 0;
    const auto upperBound = 10;
    const auto step = 2;
    const auto ints = range(lowerBound, upperBound, step);
    EXPECT_THAT(ints, ContainerEq(std::vector<int>{ 0,2,4,6,8 }));
}

TEST(RangeTest, starts_from_lower_bound)
{
    const auto lowerBound = 2;
    const auto upperBound = 10;
    const auto step = 2;
    const auto ints = range(lowerBound, upperBound, step);
    EXPECT_THAT(ints.front(), Eq(lowerBound));
}

TEST(RangeTest, ends_before_upper_bound)
{
    const auto lowerBound = 0;
    const auto upperBound = 10;
    const auto step = 2;
    const auto ints = range(lowerBound, upperBound, step);
    EXPECT_THAT(ints.back(), Lt(upperBound));
}

TEST(RangeTest, uses_default_step_one)
{
    const auto lowerBound = 0.f;
    const auto upperBound = 2.f;
    const auto floats = range(lowerBound, upperBound);
    EXPECT_EQ(floats.size(), 2);
    EXPECT_THAT(floats.front(), FloatEq(0.f));
    EXPECT_THAT(floats.back(), FloatEq(1.f));
}

TEST(RangeTest, uses_default_lower_bound_zero)
{
    const auto upperBound = 1.f;
    const auto floats = range(upperBound);
    EXPECT_EQ(floats.size(), 1);
    EXPECT_THAT(floats.front(), FloatEq(0.f));
}
}
