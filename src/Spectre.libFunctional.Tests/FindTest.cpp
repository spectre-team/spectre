/*
* FindTest.cpp
* Tests find function.
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
#include <span.h>
#include "Spectre.libFunctional/Find.h"

namespace
{
using namespace testing;
using namespace Spectre::libPlatform::Functional;

TEST(BoolFindTest, finds_nothing_in_false_vector)
{
    const std::vector<bool> falses{ false, false, false };
    const auto trueIndexes = find(falses);
    EXPECT_EQ(trueIndexes.size(), 0);
}

TEST(BoolFindTest, finds_all_in_true_vector)
{
    const std::vector<bool> trues{ true, true, true };
    const auto trueIndexes = find(trues);
    EXPECT_THAT(trueIndexes, ContainerEq(std::vector<size_t>{ 0, 1, 2 }));
}

TEST(BoolFindTest, finds_in_mixed_vector)
{
    const std::vector<bool> mixed{ true, false, true };
    const auto trueIndexes = find(mixed);
    EXPECT_THAT(trueIndexes, ContainerEq(std::vector<size_t>{ 0, 2 }));
}

TEST(FindTest, finds_nothing_in_zero_vector)
{
    const std::vector<int> zeros{ 0, 0, 0 };
    const auto nonzeroIndexes = find(gsl::as_span(zeros));
    EXPECT_EQ(nonzeroIndexes.size(), 0);
}

TEST(FindTest, finds_all_in_nonzero_vector)
{
    const std::vector<int> nonzeros{ 1, 2, 3 };
    const auto nonzeroIndexes = find(gsl::as_span(nonzeros));
    EXPECT_THAT(nonzeroIndexes, ContainerEq(std::vector<size_t>{ 0, 1, 2 }));
}

TEST(FindTest, finds_in_mixed_vector)
{
    const std::vector<int> mixed{ 1, 0, 2 };
    const auto nonzeroIndexes = find(gsl::as_span(mixed));
    EXPECT_THAT(nonzeroIndexes, ContainerEq(std::vector<size_t>{ 0, 2 }));
}
}
