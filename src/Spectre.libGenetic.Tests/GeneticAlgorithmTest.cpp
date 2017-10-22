/*
* GeneticAlgorithmTest.cpp
* Tests general-purpose genetic algorithm.
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
#include "Spectre.libException/NullPointerException.h"
#include "Spectre.libGenetic/GeneticAlgorithm.h"
#include "MockOffspringGenerator.h"
#include "MockScorer.h"
#include "MockStopCondition.h"

namespace
{
using namespace ::testing;
using namespace Spectre::libGenetic;
using namespace Spectre::libException;

class GeneticAlgorithmTest: public Test
{
protected:
    std::unique_ptr<Tests::MockScorer> scorer;
    std::unique_ptr<Tests::MockOffspringGenerator> offspringGenerator;
    std::unique_ptr<Tests::MockStopCondition> stopCondition;
    const Individual firstInitial { std::vector<bool> { true, true } };
    const Individual secondInitial { std::vector<bool> { false, false } };
    const Individual firstEvolved { std::vector<bool> { true, false } };
    const Individual secondEvolved { std::vector<bool> { false, true } };
    Generation initialGeneration { std::vector<Individual> { firstInitial, secondInitial } };
    Generation evolvedGeneration { std::vector<Individual> { firstEvolved, secondEvolved } };
    const std::vector<ScoreType> scores { 1, 1 };

    void SetUp() override
    {
        scorer = std::make_unique<Tests::MockScorer>();
        offspringGenerator = std::make_unique<Tests::MockOffspringGenerator>();
        stopCondition = std::make_unique<Tests::MockStopCondition>();
    }
};

TEST_F(GeneticAlgorithmTest, initializes)
{
    EXPECT_NO_THROW(GeneticAlgorithm(std::move(offspringGenerator), std::move(scorer), std::move(stopCondition)));
}

TEST_F(GeneticAlgorithmTest, throws_for_nullptr_scorer)
{
    scorer = nullptr;
    EXPECT_THROW(GeneticAlgorithm(std::move(offspringGenerator), std::move(scorer), std::move(stopCondition)), NullPointerException);
}

TEST_F(GeneticAlgorithmTest, throws_for_nullptr_offspring_generator)
{
    offspringGenerator = nullptr;
    EXPECT_THROW(GeneticAlgorithm(std::move(offspringGenerator), std::move(scorer), std::move(stopCondition)), NullPointerException);
}

TEST_F(GeneticAlgorithmTest, throws_for_nullptr_stop_condition)
{
    stopCondition = nullptr;
    EXPECT_THROW(GeneticAlgorithm(std::move(offspringGenerator), std::move(scorer), std::move(stopCondition)), NullPointerException);
}

TEST_F(GeneticAlgorithmTest, calls_evolution_and_scoring_for_each_iteration_once)
{
    const auto numberOfIterations = 3u;

    EXPECT_CALL(*scorer, Score(_)).Times(numberOfIterations).WillRepeatedly(Return(scores));
    EXPECT_CALL(*offspringGenerator, NextFunction(_, scores)).Times(numberOfIterations).WillRepeatedly(Return(evolvedGeneration));
    Expectation allowedIterations = EXPECT_CALL(*stopCondition, CallOperator()).Times(numberOfIterations).WillRepeatedly(Return(false));
    EXPECT_CALL(*stopCondition, CallOperator()).After(allowedIterations).WillOnce(Return(true));

    GeneticAlgorithm geneticAlgorithm(std::move(offspringGenerator), std::move(scorer), std::move(stopCondition));

    geneticAlgorithm.evolve(std::move(initialGeneration));
}

TEST_F(GeneticAlgorithmTest, returns_evolved_population)
{
    const auto numberOfIterations = 3u;

    EXPECT_CALL(*scorer, Score(_)).Times(numberOfIterations).WillRepeatedly(Return(scores));
    EXPECT_CALL(*offspringGenerator, NextFunction(_, scores)).Times(numberOfIterations).WillRepeatedly(Return(evolvedGeneration));
    Expectation allowedIterations = EXPECT_CALL(*stopCondition, CallOperator()).Times(numberOfIterations).WillRepeatedly(Return(false));
    EXPECT_CALL(*stopCondition, CallOperator()).After(allowedIterations).WillOnce(Return(true));

    GeneticAlgorithm geneticAlgorithm(std::move(offspringGenerator), std::move(scorer), std::move(stopCondition));

    const auto evolved = geneticAlgorithm.evolve(std::move(initialGeneration));

    EXPECT_EQ(evolved.size(), evolvedGeneration.size());
    for (auto i = 0u; i < evolved.size(); ++i)
    {
        EXPECT_EQ(evolved[i], evolvedGeneration[i]);
    }
}

TEST_F(GeneticAlgorithmTest, does_nothing_for_zero_iterations)
{
    const auto numberOfIterations = 0u;

    EXPECT_CALL(*scorer, Score(_)).Times(numberOfIterations);
    EXPECT_CALL(*offspringGenerator, NextFunction(_, scores)).Times(numberOfIterations);
    EXPECT_CALL(*stopCondition, CallOperator()).WillOnce(Return(true));

    GeneticAlgorithm geneticAlgorithm(std::move(offspringGenerator), std::move(scorer), std::move(stopCondition));

    geneticAlgorithm.evolve(std::move(initialGeneration));
}
}
