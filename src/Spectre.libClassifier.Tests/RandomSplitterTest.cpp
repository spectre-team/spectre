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
#include <gmock/gmock-matchers.h>
#include "Spectre.libClassifier/RandomSplitter.h"
#include "Spectre.libGenetic/DataTypes.h"

namespace
{
using namespace ::testing;
using namespace Spectre::libClassifier;

TEST(RandomSplitterInitializationTest, correct_dataset_opencv_initialization)
{
    EXPECT_NO_THROW(RandomSplitter(0.7));
}

class RandomSplitterTest : public ::testing::Test
{
public:
    RandomSplitterTest()
        :dataset(data, labels),
         randomSplitter(training, seed) {}

protected:
    const Spectre::libGenetic::Seed seed = 1;
    const double training = 0.7;
    const std::vector<DataType> data{ 0.5f, 0.4f, 0.6f, 1.1f, 1.6f, 0.7f, 2.1f, 1.0f, 0.9f, 0.8f };
    // @gmrukwa: We're assuming labels are unique in tests below
    const std::vector<Label> labels{ 3, 7, 14, 2, 12, 5, 8, 4, 19, 11 };
    const OpenCvDataset dataset;
    RandomSplitter randomSplitter;

    void SetUp() override
    {
        randomSplitter = RandomSplitter(training, seed);
    }
};

TEST_F(RandomSplitterTest, split_dataset)
{
    const auto result = randomSplitter.split(dataset);
    EXPECT_EQ(result.trainingSet.size() + result.testSet.size(), labels.size());
}

TEST_F(RandomSplitterTest, check_size_of_splitted_vectors)
{
    const auto result = randomSplitter.split(dataset);
    EXPECT_EQ(result.trainingSet.size(), data.size()*training);
    EXPECT_EQ(result.testSet.size(), data.size()- data.size()*training);
}

TEST_F(RandomSplitterTest, splits_differently_for_different_seeds)
{
    randomSplitter = RandomSplitter(training, seed);
    const auto first = randomSplitter.split(dataset);
    randomSplitter = RandomSplitter(training, seed + 1);
    const auto second = randomSplitter.split(dataset);
    for (auto i = 0u; i < first.trainingSet.size(); ++i)
    {
        if(first.trainingSet.GetSampleMetadata(i) != second.trainingSet.GetSampleMetadata(i))
        {
            SUCCEED();
        }
    }
    FAIL() << "Outputs for different seeds are exactly the same.";
}
}
