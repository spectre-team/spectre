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
#include "Spectre.libClassifier/ExcessiveTrainingRateException.h"
#include "Spectre.libClassifier/NegativeTrainingRateException.h"
#include "Spectre.libException/EmptyArgumentException.h"

namespace
{
using namespace ::testing;
using namespace Spectre::libClassifier;

TEST(RandomSplitterInitializationTest, correct_random_splitter_initialization)
{
    EXPECT_NO_THROW(RandomSplitter(0.7));
}

TEST(RandomSplitterInitializationTest, initialization_with_excessive_error)
{
    EXPECT_THROW(RandomSplitter(1.2), ExcessiveTrainingRateException);
}

TEST(RandomSplitterInitializationTest, initialization_with_negative_error)
{
    EXPECT_THROW(RandomSplitter(-0.4), NegativeTrainingRateException);
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
    RandomSplitter firstSplitter(training, seed);
    const auto first = firstSplitter.split(dataset);
    RandomSplitter secondSplitter(training, seed + 1);
    const auto second = secondSplitter.split(dataset);
    bool differs = false;
    for (auto i = 0u; i < first.trainingSet.size(); ++i)
    {
        if(first.trainingSet.GetSampleMetadata(i) != second.trainingSet.GetSampleMetadata(i))
        {
            differs = true;
            break;
        }
    }
    EXPECT_TRUE(differs) << "Outputs for different seeds are exactly the same.";
}

TEST_F(RandomSplitterTest, splits_consistently_for_the_same_seed_with_different_splitters)
{
    RandomSplitter firstSplitter(training, seed);
    const auto first = firstSplitter.split(dataset);
    RandomSplitter secondSplitter(training, seed);
    const auto second = secondSplitter.split(dataset);
    for (auto i = 0u; i < first.trainingSet.size(); ++i)
    {
        if (first.trainingSet.GetSampleMetadata(i) != second.trainingSet.GetSampleMetadata(i))
        {
            FAIL() << "Outputs for the same seeds differ.";
        }
    }
}

TEST_F(RandomSplitterTest, splits_consistently_for_the_same_seed_with_the_same_splitter)
{
    const auto first = randomSplitter.split(dataset);
    const auto second = randomSplitter.split(dataset);
    for (auto i = 0u; i < first.trainingSet.size(); ++i)
    {
        if (first.trainingSet.GetSampleMetadata(i) != second.trainingSet.GetSampleMetadata(i))
        {
            FAIL() << "Outputs for the same seeds differ.";
        }
    }
}
}
