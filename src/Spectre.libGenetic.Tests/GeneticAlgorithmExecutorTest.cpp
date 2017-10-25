/*
* GeneticAlgorithmExecutorTest.cpp
* Tests GeneticAlgorithmExecutor class
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
#include "Spectre.libGenetic/GeneticTrainingSetSelectionScenario.h"
#include "Spectre.libGenetic/RaportGenerator.h"

namespace
{
using namespace Spectre::libGenetic;

const double TRAINING_SET_SPLIT_RATE = 0.7;
const double MUTATION_RATE = 0.5;
const double BIT_SWAP_RATE = 0.5;
const double PRESERVATION_RATE = 0.5;
const unsigned int GENERATIONS_NUMBER = 20u;
const unsigned int POPULATION_SIZE = 30u;
const unsigned int INITIAL_FILLUP = 5u;
const unsigned int NUMBER_OF_RESTARTS = 1u;
const Seed SEED = 1;

TEST(GeneticTrainingSetSelectionScenarioInitialization, initializes)
{
    EXPECT_NO_THROW(Spectre::libGenetic::GeneticTrainingSetSelectionScenario(TRAINING_SET_SPLIT_RATE,
                                                                             MUTATION_RATE,
                                                                             BIT_SWAP_RATE,
                                                                             PRESERVATION_RATE, 
                                                                             GENERATIONS_NUMBER,
                                                                             { POPULATION_SIZE },
                                                                             { INITIAL_FILLUP },
                                                                             "initialize_test",
                                                                             NUMBER_OF_RESTARTS));
}

class GeneticTrainingSetSelectionScenarioInitializationTest : public ::testing::Test
{
public:
    GeneticTrainingSetSelectionScenarioInitializationTest() {}

protected:
    const std::vector<Spectre::libClassifier::DataType> data{
        0.5f, 0.4f, 0.6f,
        1.1f, 1.6f, 0.7f,
        2.1f, 1.0f, 0.6f,
        0.4f, 1.6f, 0.9f,
        1.2f, 2.2f, 0.7f,
        1.3f, 2.0f, 1.4f,
        0.7f, 0.7f, 0.9f,
        0.8f, 0.3f, 1.2f,
        0.7f, 1.9f, 0.2f,
        1.2f, 1.3f, 1.2f
    };
    const std::vector<Spectre::libClassifier::Label> labels{ 1, 1, -1, 1, -1, -1, 1, 1, -1, -1 };
    Spectre::libClassifier::OpenCvDataset dataSet = Spectre::libClassifier::OpenCvDataset(data, labels);

    void SetUp() override
    {
    }
};

TEST_F(GeneticTrainingSetSelectionScenarioInitializationTest, few_data_scenario)
{
    GeneticTrainingSetSelectionScenario scenario(TRAINING_SET_SPLIT_RATE,
                                                 MUTATION_RATE,
                                                 BIT_SWAP_RATE,
                                                 PRESERVATION_RATE,
                                                 GENERATIONS_NUMBER,
                                                 { POPULATION_SIZE },
                                                 { INITIAL_FILLUP },
                                                 "few_data_scenario",
                                                 NUMBER_OF_RESTARTS,
                                                 SEED);
    EXPECT_NO_THROW(scenario.execute(std::move(dataSet)));
}

}
