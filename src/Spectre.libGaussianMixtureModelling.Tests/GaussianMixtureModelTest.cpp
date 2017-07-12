#define GTEST_LANG_CXX11 1

#include <gtest/gtest.h>
#include "GaussianMixtureModel.h"

namespace Spectre::libGaussianMixtureModelling
{
    class GaussianMixtureModelTest : public ::testing::Test
    {
    protected:

        std::vector<double> testData;

        virtual void SetUp() override
        {
            testData = { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0 };
        }
    };

    TEST_F(GaussianMixtureModelTest, test_data_sizes)
    {
        constexpr unsigned int numberOfComponents = 5;
        GaussianMixtureModel model(testData, testData, numberOfComponents);
        EXPECT_EQ(model.components.size(), numberOfComponents);
        EXPECT_EQ(model.originalMeanSpectrum.size(), testData.size());
        EXPECT_EQ(model.originalMzArray.size(), testData.size());
    }

    TEST_F(GaussianMixtureModelTest, test_original_data_correctness)
    {
        constexpr unsigned int numberOfComponents = 5;
        GaussianMixtureModel model(testData, testData, numberOfComponents);
        for (int i = 0; i < testData.size(); i++)
        {
            EXPECT_EQ(model.originalMeanSpectrum[i], testData[i]);
            EXPECT_EQ(model.originalMzArray[i], testData[i]);
        }
        EXPECT_EQ(model.isMerged, false);
        EXPECT_EQ(model.isNoiseReduced, false);
        EXPECT_EQ(model.mzMergingThreshold, 0.0);
    }
}