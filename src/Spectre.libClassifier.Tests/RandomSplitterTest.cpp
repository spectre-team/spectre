/*
* RandomSplitterTest.cpp
* Tests RandomSplitter
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
#include "Spectre.libClassifier/RandomSplitter.h"
#include "Spectre.libGenetic/DataTypes.h"

namespace
{
using namespace Spectre::libClassifier;

TEST(RandomSplitterInitializationTest, correct_dataset_opencv_initialization)
{
    EXPECT_NO_THROW(RandomSplitter(0.7));
}

class RandomSplitterTest : public ::testing::Test
{
public:
    RandomSplitterTest()
        :randomSplitter(training, seed) {}

protected:
    const Spectre::libGenetic::Seed seed = 1;
    const double training = 0.7;
    RandomSplitter randomSplitter;

    void SetUp() override
    {
        randomSplitter = RandomSplitter(training, seed);
    }
};

TEST_F(RandomSplitterTest, split_dataset)
{
    const std::vector<DataType> data{ 0.5f, 0.4f, 0.6f, 1.1f, 1.6f, 0.7f, 2.1f, 1.0f, 0.9f, 0.8f };
    const std::vector<Label> labels{ 3, 7, 14, 2, 12, 5, 8, 4, 19, 11 };
    OpenCvDataset dataset(data, labels);
    std::pair<Spectre::libClassifier::OpenCvDataset, Spectre::libClassifier::OpenCvDataset> result = randomSplitter.split(dataset);
    EXPECT_EQ(result.first.size() + result.second.size(), labels.size());
}

TEST_F(RandomSplitterTest, check)
{
    const std::vector<DataType> data{ 0.5f, 0.4f, 0.6f, 1.1f, 1.6f, 0.7f, 2.1f, 1.0f, 0.9f, 0.8f };
    const std::vector<Label> labels{ 3, 7, 14, 2, 12, 5, 8, 4, 19, 11 };
    OpenCvDataset dataset(data, labels);
    int firstSize = 0, secondSize = 0;
    const int count = 10000;
    for (auto i = 0; i < count; i++)
    {
        std::pair<Spectre::libClassifier::OpenCvDataset, Spectre::libClassifier::OpenCvDataset> result = randomSplitter.split(dataset);
        firstSize += result.first.size();
        secondSize += result.first.size();
    }
    EXPECT_NEAR(firstSize / count, 7, 1);
    EXPECT_NEAR(secondSize / count, 3, 1);
}

}
