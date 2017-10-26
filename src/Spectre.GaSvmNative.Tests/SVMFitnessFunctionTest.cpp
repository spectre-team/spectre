/*
* SVMFitnessFunctionTest.cpp
* Tests SVMFitnessFunction.
*
Copyright 2017 Wojciech Wilgierz

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
#include "Spectre.libException/InconsistentArgumentSizesException.h"
#include "Spectre.libPlatform/Filter.h"
#include "Spectre.GaSvmNative/SVMFitnessFunction.h"

namespace
{
using namespace Spectre::libClassifier;
using namespace Spectre::GaSvmNative;

class SVMFitnessFunctionTest : public ::testing::Test
{
public:
    SVMFitnessFunctionTest():
        m_ReportGenerator("SVMFitnessFunctionTest", 2)
    {};

protected:
    const std::vector<DataType> training_data{ 0.5f, 0.4f, 0.6f, 1.1f, 1.6f, 0.7f, 2.1f, 1.0f, 0.6f,
                                               0.4f, 1.6f, 0.9f, 1.2f, 2.2f, 0.7f, 1.3f, 2.0f, 1.4f, 0.7f, 0.7f, 0.9f};
    const std::vector<DataType> test_data{ 0.8f, 0.3f, 1.2f, 0.7f, 1.9f, 0.2f, 1.2f, 1.3f, 1.2f };
    const std::vector<Label> training_labels{ 1, 1, 0, 1, 0, 0, 1 };
    const std::vector<Label> test_labels{ 1, 0, 0 };
    OpenCvDataset trainingSet = OpenCvDataset(training_data, training_labels);
    OpenCvDataset testSet = OpenCvDataset(test_data, test_labels);
    SplittedOpenCvDataset data = SplittedOpenCvDataset(std::move(trainingSet), std::move(testSet));
    RaportGenerator m_ReportGenerator;

    void SetUp() override
    {
    }

    void TearDown() override
    {
    }
};

TEST_F(SVMFitnessFunctionTest, correct_svm_initialization)
{
    EXPECT_NO_THROW(SVMFitnessFunction::SVMFitnessFunction(std::move(data), m_ReportGenerator));
}

TEST_F(SVMFitnessFunctionTest, svm_fit)
{
    SVMFitnessFunction svm(std::move(data), m_ReportGenerator);
    Spectre::libGenetic::Individual individual(std::vector<bool> { true, false, true, true, false, false, true });
    EXPECT_NO_THROW(svm.computeFitness(individual));
}

TEST_F(SVMFitnessFunctionTest, throws_when_fitting_svm_on_inconsistent_size_individual)
{
    SVMFitnessFunction svm(std::move(data), m_ReportGenerator);
    Spectre::libGenetic::Individual too_short_individual({ true, false, true, true, false, true });
    EXPECT_THROW(svm.computeFitness(too_short_individual), Spectre::libException::InconsistentArgumentSizesException);
}

}
