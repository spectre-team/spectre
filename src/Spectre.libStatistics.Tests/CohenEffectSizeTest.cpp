/*
* CohenEffectSizeTestTest.cpp
* Tests CohenEffectSize class.
*
Copyright 2017 Spectre Team

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
#include "Spectre.libStatistics/CohenEffectSize.h"
#include "Spectre.libException/EmptyArgumentException.h"

namespace
{
using namespace ::testing;
using namespace Spectre::libStatistics;
using namespace statistical_testing;
using namespace Spectre::libException;

const std::vector<PrecisionType> first { 1.,2.,3. };
const std::vector<PrecisionType> second { 10., 20., 30. };
const std::vector<PrecisionType> empty {};
const std::vector<PrecisionType> firstSingle { 1. };
const std::vector<PrecisionType> secondSingle { 10. };

CohenEffectSize estimator;

TEST(CohenEffectSize, throws_for_empty_inputs)
{
    EXPECT_THROW(estimator.Compare(empty, second), EmptyArgumentException);
    EXPECT_THROW(estimator.Compare(first, empty), EmptyArgumentException);
}

TEST(CohenEffectSize, returns_zero_for_two_single_element_collections)
{
    const auto result = estimator.Compare(firstSingle, secondSingle);
    EXPECT_THAT(result.value, FloatNear(0.0f, 1e-4f));
    EXPECT_THAT(result.strength, Eq(0u));
    EXPECT_THAT(result.interpretation, StrEq("None"));
}

TEST(CohenEffectSize, computes_reasonable_value)
{
    const auto result = estimator.Compare(first, second);
    EXPECT_THAT(result.value, FloatNear(2.06815f, 1e-5f));
    EXPECT_THAT(result.strength, Eq(4u));
    EXPECT_THAT(result.interpretation, StrEq("Very Large"));
}
}
