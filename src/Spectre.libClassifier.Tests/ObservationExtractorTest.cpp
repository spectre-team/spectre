/*
* OpenCvDatasetTest.cpp
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
#include "Spectre.libClassifier/OpenCvDataset.h"
#include "Spectre.libGenetic/Individual.h"
#include "Spectre.libGenetic/InconsistentGenerationAndScoresLengthException.h"
#include "Spectre.libClassifier/ObservationExtractor.h"

namespace
{
using namespace Spectre::libClassifier;
using namespace Spectre::libGenetic;

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
    EXPECT_NO_THROW(ObservationExtractor(dataset));
}

class ObservationExtractorTest : public ::testing::Test
{
public:
    ObservationExtractorTest() :
        dataset(OpenCvDataset(data, labels)),
        individual({ true, false, true }),
        extractor(){};

protected:
    const std::vector<DataType> data{ 0.5f, 0.4f, 0.6f, 1.1f, 1.6f, 0.7f, 2.1f, 1.0f, 0.6f };
    const std::vector<Label> labels{ 3, 7, 14 };
    Individual individual;
    OpenCvDataset dataset;
    std::unique_ptr<ObservationExtractor> extractor;

    void SetUp() override
    {
        dataset = OpenCvDataset(data, labels);
        individual = Individual({ true, false, true });
        extractor = std::make_unique<ObservationExtractor>(&dataset);
    }
};

TEST_F(ObservationExtractorTest, get_test_individual_data)
{
    DataPointer result = extractor->getOpenCVDatasetFromIndividual(individual);
    const std::vector<DataType> row1Test{ 0.5f, 0.4f, 0.6f };
    const std::vector<DataType> row2Test{ 2.1f, 1.0f, 0.6f };
    std::vector<Label> labelTest{ 3, 14 };
    Observation row1Data = (*result)[0];
    Observation row2Data = (*result)[1];
    gsl::span<Label> labelData = result->GetSampleMetadata();
    for (auto i = 0u; i < row1Data.size(); i++)
    {
        EXPECT_EQ(row1Data[i], row1Test[i]);
        EXPECT_EQ(row2Data[i], row2Test[i]);
    }
    for (auto i = 0u; i < labelData.size(); i++)
    {
        EXPECT_EQ(labelData[i], labelTest[i]);
    }
}

}
