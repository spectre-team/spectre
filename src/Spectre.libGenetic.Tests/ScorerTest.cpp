/*
* ScorerTest.cpp
* Tests Scorer.
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

#include <gtest/gtest.h>
#include <memory>
#include "Spectre.libException/NullPointerException.h"
#include "Spectre.libGenetic/Scorer.h"
#include "MockFitnessFunction.h"

namespace
{
using namespace Spectre::libGenetic;
using namespace Spectre::libGenetic::Tests;
using namespace ::testing;

TEST(Scorer, initializes)
{
    EXPECT_NO_THROW(Scorer(std::move(std::make_unique<MockFitnessFunction>())));
}

TEST(Scorer, throws_for_null_fitness_function)
{
    EXPECT_THROW(Scorer(nullptr), Spectre::libException::NullPointerException);
}

TEST(Scorer, calls_fitness_function_for_each_individual_exactly_once)
{
    const Individual individual1({ true });
    const Individual individual2({ false });
    const Generation generation({ individual1, individual2 });
    
    auto fitnessFunction = std::make_unique<MockFitnessFunction>();

    EXPECT_CALL(*fitnessFunction, CallOperator(individual1)).Times(1);
    EXPECT_CALL(*fitnessFunction, CallOperator(individual2)).Times(1);

    Scorer scorer(std::move(fitnessFunction));

    scorer.Score(generation);
}
}
