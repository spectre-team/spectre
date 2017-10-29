/*
* ObservationExtractorTest.cpp
* Tests ObservationExtractor
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
#include "Spectre.libException/NullPointerException.h"
#include "Spectre.libClassifier/OpenCvDataset.h"
#include "Spectre.libClassifier/ObservationExtractor.h"

namespace
{
using namespace Spectre::libClassifier;
using namespace Spectre::libException;

class ObservationExtractorInitializationTest : public ::testing::Test
{
protected:
    const std::vector<DataType> data{ 0.5f, 0.4f, 0.6f, 1.1f, 1.6f, 0.7f, 2.1f, 1.0f, 0.6f };
    const std::vector<Label> labels{ 3, 7, 14 };
    std::unique_ptr<OpenCvDataset> dataset;
};

TEST_F(ObservationExtractorInitializationTest, initialize_observation_extractor_with_no_errors)
{
    dataset = std::make_unique<OpenCvDataset>(data, labels);
    EXPECT_NO_THROW(ObservationExtractor(dataset.get()));
}

TEST_F(ObservationExtractorInitializationTest, throw_for_null_data)
{
    EXPECT_THROW(ObservationExtractor(nullptr), NullPointerException);
}

class ObservationExtractorTest : public ::testing::Test
{
public:
    ObservationExtractorTest() :
        dataset(data, labels)
    {
        
    };

protected:
    const std::vector<DataType> data{ 0.5f, 0.4f, 0.6f, 1.1f, 1.6f, 0.7f, 2.1f, 1.0f, 0.6f };
    const std::vector<Label> labels{ 3, 7, 14 };
    const std::vector<DataType> expectedFilteredRow0{ 0.5f, 0.4f, 0.6f };
    const std::vector<DataType> expectedFilteredRow1{ 2.1f, 1.0f, 0.6f };
    const unsigned trueBits = 2;
    size_t rowSize = data.size() / labels.size();
    const std::vector<bool> individual{ true, false, true };
    const OpenCvDataset dataset;
    std::unique_ptr<ObservationExtractor> extractor;

    void SetUp() override
    {
        extractor = std::make_unique<ObservationExtractor>(&dataset);
    }
};

TEST_F(ObservationExtractorTest, returns_dataset_of_expected_size)
{
    auto result = extractor->getOpenCvDatasetFromIndividual(individual);
    EXPECT_EQ(trueBits, result.size());
}

TEST_F(ObservationExtractorTest, observations_have_proper_size)
{
    auto result = extractor->getOpenCvDatasetFromIndividual(individual);
    EXPECT_EQ(rowSize, result[0].size());
    EXPECT_EQ(rowSize, result[1].size());
}

TEST_F(ObservationExtractorTest, observations_are_rewritten)
{
    auto result = extractor->getOpenCvDatasetFromIndividual(individual);
    const Observation row0Data = result[0];
    const Observation row1Data = result[1];
    for (auto i = 0u; i < row0Data.size(); ++i)
    {
        EXPECT_EQ(expectedFilteredRow0[i], row0Data[i]);
        EXPECT_EQ(expectedFilteredRow1[i], row1Data[i]);
    }
}

TEST_F(ObservationExtractorTest, labels_are_rewritten)
{
    auto result = extractor->getOpenCvDatasetFromIndividual(individual);
    std::vector<Label> labelTest{ 3, 14 };
    gsl::span<const Label> labelData = result.GetSampleMetadata();
    for (auto i = 0u; i < labelData.size(); i++)
    {
        EXPECT_EQ(labelTest[i], labelData[i]);
    }
}
}
