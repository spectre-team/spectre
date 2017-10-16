/*
* SVMTest.cpp
* Tests SVM
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
#include "Spectre.libClassifier/SVMFitnessFunction.h"
#include "Spectre.libException/InconsistentArgumentSizesException.h"
#include "Spectre.libPlatform/Filter.h"

namespace
{
using namespace Spectre::libClassifier;

class SVMFitnessFunctionTest : public ::testing::Test
{
public:
    SVMFitnessFunctionTest() {};

protected:
    const std::vector<DataType> training_data{ 0.5f, 0.4f, 0.6f, 1.1f, 1.6f, 0.7f, 2.1f, 1.0f, 0.6f,
                                               0.4f, 1.6f, 0.9f, 1.2f, 2.2f, 0.7f, 1.3f, 2.0f, 1.4f, 0.7f, 0.7f, 0.9f};
    const std::vector<DataType> test_data{ 0.8f, 0.3f, 1.2f, 0.7f, 1.9f, 0.2f, 1.2f, 1.3f, 1.2f };
    const std::vector<Label> training_labels{ 1, 1, -1, 1, -1, -1, 1 };
    const std::vector<Label> test_labels{ 1, -1, -1 };
    OpenCvDataset trainingSet = OpenCvDataset(training_data, training_labels);
    OpenCvDataset testSet = OpenCvDataset(test_data, test_labels);

    void SetUp() override
    {
    }
};

TEST_F(SVMFitnessFunctionTest, correct_svm_initialization)
{
    SplittedOpenCvDataset data = SplittedOpenCvDataset(std::move(trainingSet), std::move(testSet));
    EXPECT_NO_THROW(SVMFitnessFunction::SVMFitnessFunction(std::move(data)));
}

TEST_F(SVMFitnessFunctionTest, svm_fit)
{
    SplittedOpenCvDataset data = SplittedOpenCvDataset(std::move(trainingSet), std::move(testSet));
    SVMFitnessFunction svm(std::move(data));
    Spectre::libGenetic::Individual individual(std::vector<bool> { true, false, true, true, false, false, true });
    EXPECT_NO_THROW(svm.fit(individual));
}

TEST_F(SVMFitnessFunctionTest, svm_fit_with_inconsistent_size)
{
    SplittedOpenCvDataset data = SplittedOpenCvDataset(std::move(trainingSet), std::move(testSet));
    SVMFitnessFunction svm(std::move(data));
    Spectre::libGenetic::Individual too_short_individual({ true, false, true, true, false, true });
    EXPECT_THROW(svm.fit(too_short_individual), Spectre::libException::InconsistentArgumentSizesException);
}

}