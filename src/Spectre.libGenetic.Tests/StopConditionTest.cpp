/*
* StopConditionTest.cpp
* Tests StopCondition
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
#include "Spectre.libGenetic/StopCondition.h"

namespace
{
using namespace Spectre::libGenetic;

TEST(StopCondition, initializes)
{
    EXPECT_NO_THROW(StopCondition(10));
}

TEST(StopCondition, iterates_fixed_number_of_times)
{
    const auto ITERATIONS_NUMBER = 3u;
    StopCondition stop(ITERATIONS_NUMBER);
    for (auto i = 0u; i < ITERATIONS_NUMBER; ++i)
    {
        EXPECT_FALSE(stop());
    }
    EXPECT_TRUE(stop());
}
}
