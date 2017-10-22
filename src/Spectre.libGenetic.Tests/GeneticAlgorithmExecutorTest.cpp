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

namespace
{
using namespace Spectre::libGenetic;

TEST(GeneticTrainingSetSelectionScenarioInitialization, initializes)
{
    EXPECT_NO_THROW(Spectre::libGenetic::GeneticTrainingSetSelectionScenario(0.7, 0.5, 0.5, 0.5, 20, 30, 5, 1));
}

class GeneticTrainingSetSelectionScenarioInitializationTest : public ::testing::Test
{
public:
    GeneticTrainingSetSelectionScenarioInitializationTest() {}

protected:
    const std::vector<Spectre::libClassifier::DataType> data{ 0.5f, 0.4f, 0.6f, 1.1f, 1.6f, 0.7f, 2.1f, 1.0f, 0.6f,
        0.4f, 1.6f, 0.9f, 1.2f, 2.2f, 0.7f, 1.3f, 2.0f, 1.4f, 0.7f, 0.7f, 0.9f, 0.8f, 0.3f, 1.2f, 0.7f, 1.9f, 0.2f, 1.2f, 1.3f, 1.2f };
    const std::vector<Spectre::libClassifier::Label> labels{ 1, 1, -1, 1, -1, -1, 1, 1, -1, -1 };
    Spectre::libClassifier::OpenCvDataset dataSet = Spectre::libClassifier::OpenCvDataset(data, labels);

    void SetUp() override
    {
    }
};

TEST_F(GeneticTrainingSetSelectionScenarioInitializationTest, few_data_scenario)
{
    GeneticTrainingSetSelectionScenario scenario(0.7, 0.5, 0.5, 0.5, 20, 30, 5, 1);
    Generation generation = scenario.execute(std::move(dataSet));
    EXPECT_EQ(generation.size(), 30);
}

}