/*
* StatisticalIndexTest.cpp
* Tests StatisticalIndex class.
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
#include "Spectre.libStatistics/StatisticalIndex.h"

namespace
{
using namespace Spectre::libStatistics;
using namespace statistical_testing;
using namespace ::testing;

const std::string INTERPRETATION = "BLAH";
const PrecisionType VALUE = 1.0;
const unsigned STRENGTH = 0;

TEST(StatisticalIndex, initializes)
{
    EXPECT_NO_THROW(StatisticalIndex(VALUE, STRENGTH, INTERPRETATION));
}

TEST(StatisticalIndex, keeps_value)
{
    EXPECT_THAT(StatisticalIndex(VALUE, STRENGTH, INTERPRETATION).value, FloatEq(VALUE));
}

TEST(StatisticalIndex, keeps_strength)
{
    EXPECT_THAT(StatisticalIndex(VALUE, STRENGTH, INTERPRETATION).strength, Eq(STRENGTH));
}

TEST(StatisticalIndex, keeps_interpretation)
{
    EXPECT_THAT(StatisticalIndex(VALUE, STRENGTH, INTERPRETATION).interpretation, StrEq(INTERPRETATION));
}

TEST(StatisticalIndex, interpretation_is_not_copied)
{
    EXPECT_THAT(&StatisticalIndex(VALUE, STRENGTH, INTERPRETATION).interpretation, &INTERPRETATION);
}
}
