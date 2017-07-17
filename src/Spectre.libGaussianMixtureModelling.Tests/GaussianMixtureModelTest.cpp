/*
* GaussianMixtureModelTest.cpp
* Provides implementation of tests for the purpose of testing
* Gaussian Mixture Modelling.
*
Copyright 2017 Michal Gallus

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
#define _CRT_RAND_S  

#include <gtest/gtest.h>
#include <algorithm>
#include <stdlib.h>
#include "GaussianMixtureModel.h"

namespace Spectre::libGaussianMixtureModelling
{
    class GaussianMixtureModelTest : public ::testing::Test
    {
    protected:

        std::vector<double> testData;
        std::vector<GaussianComponent> gaussianComponents;

        virtual void SetUp() override
        {
            testData = { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0 };
            gaussianComponents = {
                { -5.0, 3.0, 0.25 },
                { 5.0, 3.0, 0.25 },
                { -2.0, 9.0, 0.5 }
            };
        }
    };

    TEST_F(GaussianMixtureModelTest, test_data_sizes)
    {
        GaussianMixtureModel model(testData, testData, std::move(gaussianComponents));
        EXPECT_EQ(model.components.size(), gaussianComponents.size());
        EXPECT_EQ(model.originalMeanSpectrum.size(), testData.size());
        EXPECT_EQ(model.originalMzArray.size(), testData.size());
    }

    TEST_F(GaussianMixtureModelTest, test_original_data_correctness)
    {
        GaussianMixtureModel model(testData, testData, std::move(gaussianComponents));
        for (int i = 0; i < testData.size(); i++)
        {
            EXPECT_EQ(model.originalMeanSpectrum[i], testData[i]);
            EXPECT_EQ(model.originalMzArray[i], testData[i]);
        }
        for (int i = 0; i < gaussianComponents.size(); i++)
        {
            EXPECT_EQ(model.components[i].deviation, gaussianComponents[i].deviation);
            EXPECT_EQ(model.components[i].mean, gaussianComponents[i].mean);
            EXPECT_EQ(model.components[i].weight, gaussianComponents[i].weight);

        }
        EXPECT_EQ(model.isMerged, false);
        EXPECT_EQ(model.isNoiseReduced, false);
        EXPECT_EQ(model.mzMergingThreshold, 0.0);
    }
}