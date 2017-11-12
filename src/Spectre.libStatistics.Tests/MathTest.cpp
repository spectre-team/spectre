/*
* MathTest.cpp
* Tests basic vector arithmetics.
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
#include "Spectre.libStatistics/Math.h"

namespace
{
using namespace testing;
using namespace Spectre::libStatistics;

using Data = const std::vector<double>;
using IntData = const std::vector<int>;

Data lowerData{ 1,2,3 };
Data greaterData{ 4,5,6 };

gsl::span<const double> lower(lowerData);
gsl::span<const double> greater(greaterData);

// @gmrukwa: vector vs vector

TEST(VectorPlusTest, adds)
{
    const auto sum = plus(lower, greater);
    EXPECT_THAT(sum, ContainerEq(Data{ 5,7,9 }));
}

TEST(VectorMinusTest, subtracts)
{
    const auto difference = minus(greater, lower);
    EXPECT_THAT(difference, ContainerEq(Data{ 3,3,3 }));
}

TEST(VectorMultiplyTest, multiplies_elementwise)
{
    const auto multiple = multiplyBy(greater, lower);
    EXPECT_THAT(multiple, ContainerEq(Data{ 4, 10, 18 }));
}

TEST(VectorDivideTestt, divides_elementwise)
{
    const auto division = divideBy(greater, lower);
    EXPECT_THAT(division, ContainerEq(Data{ 4, 2.5, 2 }));
}

TEST(VectorModuloTest, finds_rest_elementwise)
{
    const std::vector<int> simpleLower{ 1,2,3 };
    const std::vector<int> simpleGreater{ 4,5,6 };
    const auto remainders = modulo(gsl::as_span(simpleGreater), gsl::as_span(simpleLower));
    EXPECT_THAT(remainders, ContainerEq(IntData{ 0, 1, 0 }));
}

TEST(VectorBitwiseAndTest, ands_bits_elementwise)
{
    const std::vector<int> simpleLower{ 1,2,3 };
    const std::vector<int> simpleGreater{ 4,5,6 };
    const auto anded = bitwiseAnd(gsl::as_span(simpleLower), gsl::as_span(simpleGreater));
    EXPECT_THAT(anded, ContainerEq(IntData{ 0, 0, 2 }));
}

TEST(VectorLogicalAndTest, ands_logically_elementwise)
{
    const std::vector<int> simpleLower{ 1,0,3 };
    const std::vector<int> simpleGreater{ 4,5,6 };
    const auto anded = logicalAnd(gsl::as_span(simpleLower), gsl::as_span(simpleGreater));
    EXPECT_THAT(anded, ContainerEq(IntData{ 1, 0, 1 }));
}

TEST(VectorBitwiseOrTest, ors_bits_elementwise)
{
    const std::vector<int> simpleLower{ 1,2,3 };
    const std::vector<int> simpleGreater{ 4,5,6 };
    const auto ored = bitwiseOr(gsl::as_span(simpleLower), gsl::as_span(simpleGreater));
    EXPECT_THAT(ored, ContainerEq(IntData{ 5, 7, 7 }));
}

TEST(VectorLogicalOrTest, ors_logically_elementwise)
{
    const std::vector<int> simpleLower{ 1,0,2 };
    const std::vector<int> simpleGreater{ 0,0,6 };
    const auto ored = logicalOr(gsl::as_span(simpleLower), gsl::as_span(simpleGreater));
    EXPECT_THAT(ored, ContainerEq(IntData{ 1, 0, 1 }));
}

TEST(VectorMaxTest, picks_max_elementwise)
{
    Data mixedLow{ 1,5,2 };
    Data mixedHigh{ 3,0,4 };
    const auto maxes = max(gsl::as_span(mixedLow), gsl::as_span(mixedHigh));
    EXPECT_THAT(maxes, ContainerEq(Data{ 3,5,4 }));
}

TEST(VectorMinTest, picks_min_elementwise)
{
    Data mixedLow{ 1,5,2 };
    Data mixedHigh{ 3,0,4 };
    const auto mins = min(gsl::as_span(mixedLow), gsl::as_span(mixedHigh));
    EXPECT_THAT(mins, ContainerEq(Data{ 1,0,2 }));
}

TEST(VectorEqualsTest, finds_equal_elements)
{
    // @gmrukwa: std::vector<bool> is not convertible to gsl::span<bool> due to std::vector<bool> template specialization
    const std::vector<int> first{ true, false, true };
    const std::vector<int> second{ false, false, true };
    const auto equalities = equals(gsl::as_span(first), gsl::as_span(second));
    EXPECT_THAT(equalities, ContainerEq(std::vector<bool>{false, true, true}));
}

// @gmrukwa: vector vs scalar

TEST(ScalarPlusTest, adds_scalar_to_each_element)
{
    const auto added = plus(lower, 1.);
    EXPECT_THAT(added, ContainerEq(Data{ 2.,3.,4. }));
}

TEST(ScalarMinusTest, subtracts_scalar_from_each_element)
{
    const auto subtracted = minus(lower, 1.);
    EXPECT_THAT(subtracted, ContainerEq(Data{ 0.,1.,2. }));
}

TEST(ScalarMultiplyTest, multiplies_each_element_by_scalar)
{
    const auto multiplied = multiplyBy(lower, 3.);
    EXPECT_THAT(multiplied, ContainerEq(Data{ 3.,6.,9. }));
}

TEST(ScalarDivideTest, divides_each_element_by_scalar)
{
    const auto divided = divideBy(lower, 2.);
    EXPECT_THAT(divided, ContainerEq(Data{ .5, 1., 1.5 }));
}

TEST(ScalarModuloTest, finds_remainder_of_each_element)
{
    IntData data{ 1,2,3 };
    const auto remainders = modulo(gsl::as_span(data), 3);
    EXPECT_THAT(remainders, ContainerEq(IntData{ 1,2,0 }));
}

TEST(ScalarBitwiseAndTest, ands_bits_of_each_element)
{
    IntData data{ 1,0,7 };
    const auto anded = bitwiseAnd(gsl::as_span(data), 3);
    EXPECT_THAT(anded, ContainerEq(IntData{ 1, 0, 3 }));
}

TEST(ScalarLogicalAndTest, ands_logically_each_element)
{
    IntData data{ 1,0,7 };
    const auto anded = logicalAnd(gsl::as_span(data), 3);
    EXPECT_THAT(anded, ContainerEq(IntData{ 1, 0, 1 }));
}

TEST(ScalarBitwiseOrTest, ors_bits_of_each_element)
{
    IntData data{ 1,0,7 };
    const auto ored = bitwiseOr(gsl::as_span(data), 2);
    EXPECT_THAT(ored, ContainerEq(IntData{ 3, 2, 7 }));
}

TEST(ScalarLogicalOrTest, ors_logically_each_element)
{
    IntData data{ 1,0,7 };
    const auto ored = logicalOr(gsl::as_span(data), 0);
    EXPECT_THAT(ored, ContainerEq(IntData{ 1, 0, 1 }));
}

TEST(ScalarMaxTest, picks_max_of_element_and_scalar)
{
    IntData data{ 1,0,7 };
    const auto maxes = max(gsl::as_span(data), 1);
    EXPECT_THAT(maxes, ContainerEq(IntData{ 1, 1, 7 }));
}

TEST(ScalarMinTest, picks_min_of_element_and_scalar)
{
    IntData data{ 1,0,7 };
    const auto mins = min(gsl::as_span(data), 1);
    EXPECT_THAT(mins, ContainerEq(IntData{ 1, 0, 1 }));
}

TEST(ScalarEqualsTest, finds_equal_elements)
{
    // @gmrukwa: std::vector<bool> is not convertible to gsl::span<bool> due to std::vector<bool> template specialization
    const std::vector<int> first{ true, false, true };
    const int second = false;
    const auto equalities = equals(gsl::as_span(first), second);
    EXPECT_THAT(equalities, ContainerEq(std::vector<bool>{false, true, false}));
}

// @gmrukwa: unary

TEST(AbsTest, returns_absolute_value_of_each_element)
{
    Data data{ -1, 2, 3, -4 };
    const auto absolutes = abs(gsl::as_span(data));
    EXPECT_THAT(absolutes, ContainerEq(Data{ 1,2,3,4 }));
}
}
