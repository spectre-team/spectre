/*
* DatasetTest.cpp
* Tests of Dataset implementation.
*
Copyright 2017 Maciej Gamrat, Grzegorz Mrukwa

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

#define GTEST_LANG_CXX11 1

#include <gtest/gtest.h>
#include "Dataset.h"

using namespace Spectre::libDataset;
using samples = std::vector<int>;
using samples_metadata = std::vector<int>;
using dataset_metadata = std::vector<int>;
using data_set = Dataset<int, int, dataset_metadata>;

// @gmrukwa: provides general initialization test & emptiness tests
namespace
{
TEST(DatasetInitializationTest, should_initialize_correctly)
{
    auto data = samples{ 1, 2, 3 };
    auto sampleMetadata = samples_metadata{ 4, 5, 6 };
    auto datasetMetadata = dataset_metadata{7};

    data_set dataset(data, sampleMetadata, datasetMetadata);
}

TEST(DatasetInitializationTest, throw_on_inconsistent_input_size)
{
    auto data = samples{ 1, 2, 3 };
    auto sampleMetadata = samples_metadata{ 4, 5 };
    auto datasetMetadata = dataset_metadata{ 7 };

    EXPECT_THROW(data_set(data, sampleMetadata, datasetMetadata), InconsistentInputSize);
}

TEST(DatasetTest, knows_if_empty)
{
    samples empty_samples;
    samples_metadata empty_samples_metadata;
    dataset_metadata empty_dataset_metadata;
    data_set dataset(empty_samples, empty_samples_metadata, empty_dataset_metadata);
    EXPECT_TRUE(dataset.empty());
}

TEST(DatasetTest, loose_emptiness_on_add)
{
    samples empty_samples;
    samples_metadata empty_samples_metadata;
    dataset_metadata empty_dataset_metadata;
    data_set dataset(empty_samples, empty_samples_metadata, empty_dataset_metadata);
    ASSERT_TRUE(dataset.empty()) << "dataset was not known as empty";
    dataset.addSample(0, 1);
    EXPECT_FALSE(dataset.empty());
}
}

// @gmrukwa: provides general tests
namespace
{
/// <summary>
/// Provides tests for dataset which data source has not yet been destroyed.
/// </summary>
/// <seealso cref="::testing::Test" />
class DatasetInScopeTest : public ::testing::Test
{
public:
    DatasetInScopeTest():
        data({1,2,3}),
        entries_metadata({4,5,6}),
        global_metadata({7}),
        dataset(data, entries_metadata, global_metadata)
    { }
protected:
    const samples data;
    const samples_metadata entries_metadata;
    const dataset_metadata global_metadata;
    data_set dataset;

    void SetUp() override
    {
        dataset = data_set(data, entries_metadata, global_metadata);
    }
};

TEST_F(DatasetInScopeTest, provide_the_same_data)
{
    EXPECT_EQ(dataset[0], 1);
    EXPECT_EQ(dataset[1], 2);
    EXPECT_EQ(dataset[2], 3);
}

TEST_F(DatasetInScopeTest, provide_the_same_sample_metadata)
{
    EXPECT_EQ(dataset.getSampleMetadata(0), 4);
    EXPECT_EQ(dataset.getSampleMetadata(1), 5);
    EXPECT_EQ(dataset.getSampleMetadata(2), 6);
}

TEST_F(DatasetInScopeTest, provide_the_same_metadata)
{
    EXPECT_EQ(dataset.getDatasetMetadata()[0], 7);
}

TEST_F(DatasetInScopeTest, modify_data)
{
    ASSERT_NE(dataset[0], 100) << "data was already set to test value";
    dataset[0] = 100;
    EXPECT_EQ(dataset[0], 100);
}

TEST_F(DatasetInScopeTest, modify_sample_metadata)
{
    ASSERT_NE(dataset.getSampleMetadata(0), 100) << "metadata was already set to test value";
    dataset.getSampleMetadata(0) = 100;
    EXPECT_EQ(dataset.getSampleMetadata(0), 100);
}

TEST_F(DatasetInScopeTest, modify_metadata)
{
    ASSERT_NE(dataset.getDatasetMetadata()[0], 100) << "metadata was already set to test value";
    dataset.getDatasetMetadata() = { 100 };
    EXPECT_EQ(dataset.getDatasetMetadata()[0], 100);
}

TEST_F(DatasetInScopeTest, access_const_sample)
{
    const auto& local = dataset;
    EXPECT_EQ(local[0], dataset[0]);
}

TEST_F(DatasetInScopeTest, access_const_sample_metadata)
{
    const auto& local = dataset;
    EXPECT_EQ(local.getSampleMetadata(0), dataset.getSampleMetadata(0));
}

TEST_F(DatasetInScopeTest, modify_data_throws_when_out_of_bounds)
{
    EXPECT_THROW(dataset[100] = 100, OutOfRange);
}

TEST_F(DatasetInScopeTest, modify_sample_metadata_throws_when_out_of_bounds)
{
    EXPECT_THROW(dataset.getSampleMetadata(100) = 100, OutOfRange);
}

TEST_F(DatasetInScopeTest, access_const_sample_throws_when_out_of_bounds)
{
    const auto& local = dataset;
    EXPECT_THROW(local[100], OutOfRange);
}

TEST_F(DatasetInScopeTest, access_const_sample_metadata_throws_when_out_of_bounds)
{
    const auto& local = dataset;
    EXPECT_THROW(local.getSampleMetadata(100), OutOfRange);
}

TEST_F(DatasetInScopeTest, access_whole_data_in_readonly)
{
    const auto& local = dataset;
    auto local_data = local.getData();
    EXPECT_EQ(local_data.size(), 3);
    EXPECT_EQ(local_data[0], 1);
    EXPECT_EQ(local_data[1], 2);
    EXPECT_EQ(local_data[2], 3);
}

TEST_F(DatasetInScopeTest, access_whole_sample_metadata_in_readonly)
{
    const auto& local = dataset;
    auto local_sample_metadata = local.getSampleMetadata();
    EXPECT_EQ(local_sample_metadata.size(), 3);
    EXPECT_EQ(local_sample_metadata[0], 4);
    EXPECT_EQ(local_sample_metadata[1], 5);
    EXPECT_EQ(local_sample_metadata[2], 6);
}

TEST_F(DatasetInScopeTest, know_size)
{
    EXPECT_EQ(dataset.size(), 3);
}

TEST_F(DatasetInScopeTest, add_new_sample)
{
    dataset.addSample(8, 9);
    EXPECT_EQ(dataset.getData().size(), 4);
    EXPECT_EQ(dataset.getSampleMetadata().size(), 4);
    EXPECT_EQ(dataset.size(), 4);
    EXPECT_EQ(dataset[3], 8);
    EXPECT_EQ(dataset.getSampleMetadata(3), 9);
}
}

// @gmrukwa: provides test with destroyed initialization data
namespace
{
TEST(DatasetOutOfScopeTest, owns_data)
{
    data_set* dataset = nullptr;
    
    // @gmrukwa: Opening new scope, so variables will get destroyed.
    {
        samples data({0, 1, 2});
        samples_metadata sample_metadata({ 4, 5, 6 });
        dataset_metadata metadata({ 7 });

        dataset = new data_set(data, sample_metadata, metadata);
    }

    auto data = dataset->getData();
    for(auto i = 0; i < 3; ++i)
    {
        EXPECT_EQ(data[i], i) << "mismatch at position: " << i;
    }

    delete dataset;
}

TEST(DatasetOutOfScopeTest, owns_sample_metadata)
{
    data_set* dataset = nullptr;

    // @gmrukwa: Opening new scope, so variables will get destroyed.
    {
        samples data({ 0, 1, 2 });
        samples_metadata sample_metadata({ 4, 5, 6 });
        dataset_metadata metadata({ 7 });

        dataset = new data_set(data, sample_metadata, metadata);
    }

    auto metadata = dataset->getSampleMetadata();
    for (auto i = 0; i < 3; ++i)
    {
        EXPECT_EQ(metadata[i], i + 4) << "mismatch at position: " << i;
    }

    delete dataset;
}

TEST(DatasetOutOfScopeTest, owns_metadata)
{
    data_set* dataset = nullptr;

    // @gmrukwa: Opening new scope, so variables will get destroyed.
    {
        samples data({ 0, 1, 2 });
        samples_metadata sample_metadata({ 4, 5, 6 });
        dataset_metadata metadata({ 7 });

        dataset = new data_set(data, sample_metadata, metadata);
    }

    auto metadata = dataset->getDatasetMetadata();
    EXPECT_EQ(metadata[0], 7);

    delete dataset;
}
}