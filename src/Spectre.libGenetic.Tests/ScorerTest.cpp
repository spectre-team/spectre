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

#define GTEST_LANG_CXX11 1

#include <gtest/gtest.h>
#include <memory>
#include "Spectre.libException/NullPointerException.h"
#include "Spectre.libException/ObjectMovedException.h"
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

TEST(Scorer, move_assignment_moves_fitness_function)
{
    const Individual individual({ true, true, true });
    const Generation generation({ individual });

    auto fitnessFunction1 = std::make_unique<MockFitnessFunction>();
    auto fitnessFunction2 = std::make_unique<MockFitnessFunction>();

    EXPECT_CALL(*fitnessFunction1, CallOperator(individual)).Times(1)
        .WillRepeatedly(Return(std::vector<ScoreType>{ 1 }));
    EXPECT_CALL(*fitnessFunction1, CallOperator(individual)).Times(2)
        .WillRepeatedly(Return(std::vector<ScoreType>{ 2 }));

    Scorer scorer1(std::move(fitnessFunction1));
    Scorer scorer2(std::move(fitnessFunction2));

    EXPECT_EQ(scorer1.Score(generation), 1);
    EXPECT_EQ(scorer2.Score(generation), 2);
    
    scorer1 = std::move(scorer2);

    EXPECT_EQ(scorer1.Score(generation), 1);
}

TEST(Scorer, move_assignment_invalidates_source)
{
    const Individual individual({ true, true, true });
    const Generation generation({ individual });

    auto fitnessFunction1 = std::make_unique<MockFitnessFunction>();
    auto fitnessFunction2 = std::make_unique<MockFitnessFunction>();

    Scorer scorer1(std::move(fitnessFunction1));
    Scorer scorer2(std::move(fitnessFunction2));
    
    scorer1 = std::move(scorer2);

    EXPECT_THROW(scorer2.Score(generation), Spectre::libException::ObjectMovedException);
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
