/*
* SplittedOpenCVDatasetTest.cpp
* Tests SplittedOpenCVDataset
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

namespace
{
    using namespace Spectre::libClassifier;

    class SplittedOpenCVDatasetTest : public ::testing::Test
    {
    public:
        SplittedOpenCVDatasetTest() { }

    protected:
        const std::vector<DataType> training_data{ 0.5f, 0.4f, 0.6f, 1.1f, 1.6f, 0.7f, 2.1f, 1.0f, 0.6f,
            0.4f, 1.6f, 0.9f, 1.2f, 2.2f, 0.7f, 1.3f, 2.0f, 1.4f, 0.7f, 0.7f, 0.9f };
        const std::vector<DataType> test_data{ 0.8f, 0.3f, 1.2f, 0.7f, 1.9f, 0.2f, 1.2f, 1.3f, 1.2f };
        const std::vector<Label> training_labels{ 2, 7, 5, 9, 13, 4, 10 };
        const std::vector<Label> test_labels{ 8, 11, 5 };
        OpenCvDataset trainingSet = OpenCvDataset(training_data, training_labels);
        OpenCvDataset testSet = OpenCvDataset(test_data, test_labels);

        void SetUp() override
        {
        }
    };

    TEST_F(SplittedOpenCVDatasetTest, correct_splitted_opencv_dataset_initialization)
    {
        EXPECT_NO_THROW(SplittedOpenCvDataset(std::move(trainingSet), std::move(testSet)));
    }

    TEST_F(SplittedOpenCVDatasetTest, test_splitted_opencvdataset_data)
    {
        SplittedOpenCvDataset splittedData = SplittedOpenCvDataset(std::move(trainingSet), std::move(testSet));
        EXPECT_EQ(splittedData.trainingSet.size(), 7);
        EXPECT_EQ(splittedData.trainingSet.GetData().size(), 7);
        EXPECT_EQ(splittedData.trainingSet.getMatData().rows, 7);
        EXPECT_EQ(splittedData.trainingSet.getMatLabels().rows, 7);
        EXPECT_EQ(splittedData.testSet.size(), 3);
        EXPECT_EQ(splittedData.testSet.GetData().size(), 3);
        EXPECT_EQ(splittedData.testSet.getMatData().rows, 3);
        EXPECT_EQ(splittedData.testSet.getMatLabels().rows, 3);
    }

    TEST_F(SplittedOpenCVDatasetTest, check_correctness_of_structure_data)
    {
        SplittedOpenCvDataset data = SplittedOpenCvDataset(std::move(trainingSet), std::move(testSet));
        OpenCvDataset verifyTrainingSet = OpenCvDataset(training_data, training_labels);
        OpenCvDataset verifyTestSet = OpenCvDataset(test_data, test_labels);
        for (auto i = 0; i < verifyTrainingSet.size(); i++)
        {
            for (auto j = 0; j < verifyTrainingSet[i].size(); j++)
            {
                EXPECT_EQ(data.trainingSet[i][j], verifyTrainingSet[i][j]);
            }
        }
        for (auto i = 0; i < verifyTestSet.size(); i++)
        {
            for (auto j = 0; j < verifyTestSet[i].size(); j++)
            {
                EXPECT_EQ(data.testSet[i][j], verifyTestSet[i][j]);
            }
        }
    }


}
