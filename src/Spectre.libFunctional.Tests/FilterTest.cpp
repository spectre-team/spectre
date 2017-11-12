/*
* FilterTest.cpp
* Tests filter function.
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
#include "Spectre.libException/InconsistentArgumentSizesException.h"
#include "Spectre.libFunctional/Filter.h"

namespace
{
using namespace testing;
using namespace Spectre::libException;
using namespace Spectre::libPlatform::Functional;

using Filter = const std::vector<bool>;
using Indexes = const std::vector<size_t>;

const std::vector<int> someInts{ 1,2,3 };
gsl::span<const int> ints(someInts);

TEST(BoolFilterTest, empty_for_falses)
{
    Filter falses{ false, false, false };
    const auto filtered = filter(ints, falses);
    EXPECT_EQ(filtered.size(), 0);
}

TEST(BoolFilterTest, all_for_trues)
{
    Filter trues{ true, true, true };
    const auto filtered = filter(ints, trues);
    EXPECT_THAT(filtered, ContainerEq(someInts));
}

TEST(BoolFilterTest, selected_for_mixed)
{
    Filter mixed{ true, false, true };
    const auto filtered = filter(ints, mixed);
    EXPECT_THAT(filtered, ContainerEq(std::vector<int>{1, 3}));
}

TEST(BoolFilterTest, throws_on_inconsistent_sizes)
{
    Filter tooLong{ true, false, true, false };
    EXPECT_THROW(filter(ints, tooLong), InconsistentArgumentSizesException);
}

TEST(IndexFilterTest, throws_on_exceeding_index)
{
    Indexes exceedingIndex{ 3 };
    EXPECT_THROW(filter(ints, exceedingIndex), OutOfRangeException);
}

TEST(IndexFilterTest, empty_for_no_indexes)
{
    Indexes none{};
    const auto filtered = filter(ints, none);
    EXPECT_EQ(filtered.size(), 0);
}

TEST(IndexFilterTest, all_for_all_indexes)
{
    auto all = range(static_cast<size_t>(ints.size()));
    const auto filtered = filter(ints, all);
    EXPECT_THAT(filtered, ContainerEq(someInts));
}

TEST(IndexFilterTest, selected_for_some_indexes)
{
    Indexes some{ 0, 2 };
    const auto filtered = filter(ints, some);
    EXPECT_THAT(filtered, ContainerEq(std::vector<int>{1, 3}));
}

TEST(IndexFilterTest, allows_duplication)
{
    Indexes duplicated{ 2, 2 };
    const auto filtered = filter(ints, duplicated);
    EXPECT_THAT(filtered, ContainerEq(std::vector<int>{3, 3}));
}
}
