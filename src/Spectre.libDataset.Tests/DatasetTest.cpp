#include "stdafx.h"
#include "CppUnitTest.h"

#include <gtest/gtest.h>
#include "Dataset.h"

using namespace testing;

namespace Spectre::libDataset::Tests
{		
    TEST(libDatasetUnitTest, ShouldInitializeCorrectly)
    {
        auto data = std::vector<int>{ 1, 2, 3 };
        auto sampleMetadata = std::vector<int>{ 4, 5, 6 };
        int datasetMetadata = 7;

        Dataset<std::vector<int>, std::vector<int>, int> dataset(data, sampleMetadata, datasetMetadata);
    }
}