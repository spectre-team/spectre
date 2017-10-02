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
#include "Spectre.libClassifier/SVM.h"

namespace
{
using namespace Spectre::libClassifier;

TEST(SVMInitializationTest, correct_svm_initialization)
{
    EXPECT_NO_THROW(SVM::SVM());
}

class SVMTest : public ::testing::Test
{
public:
    SVMTest() {};

protected:
    const std::vector<DataType> training_data{ 0.5f, 0.4f, 0.6f, 1.1f, 1.6f, 0.7f, 2.1f, 1.0f, 0.6f,
                                               0.4f, 1.6f, 0.9f, 1.2f, 2.2f, 0.7f, 1.3f, 2.0f, 1.4f, 0.7f, 0.7f, 0.9f};
    const std::vector<DataType> test_data{ 0.8f, 0.3f, 1.2f, 0.7f, 1.9f, 0.2f, 1.2f, 1.3f, 1.2f };
    const std::vector<Label> training_labels{ 2, 7, 5, 9, 13, 4, 10 };
    const std::vector<Label> test_labels{ 8, 11, 5 };
    OpenCvDataset trainingSet = OpenCvDataset(training_data, training_labels);
    OpenCvDataset testSet = OpenCvDataset(test_data, test_labels);

    void SetUp() override
    {
    }
};

TEST_F(SVMTest, svm_train)
{
    SplittedOpenCvDataset data = SplittedOpenCvDataset(std::move(trainingSet), std::move(testSet));
    SVM svm = SVM();
    cv::Mat result = svm.getResult(std::move(data));
    EXPECT_EQ(result.cols*result.rows, 3);
}

}
