#define GTEST_LANG_CXX11 1

#include <gtest/gtest.h>
#include "Dataset.h"

using namespace Spectre::libDataset;

namespace
{
    TEST(DatasetTest, should_initialize_correctly)
    {
        auto data = std::vector<int>{ 1, 2, 3 };
        auto sampleMetadata = std::vector<int>{ 4, 5, 6 };
        int datasetMetadata = 7;

        Dataset<std::vector<int>, std::vector<int>, int> dataset(data, sampleMetadata, datasetMetadata);
    }
}