/*
* GaussianMixtureModelTest.cpp
* Provides implementation of tests for the purpose of testing
* Gaussian Mixture Modelling algorithms and related.
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
#include "ExpectationMaximization.h"
#include "BasicInitializationRef.h"
#include "ExpectationRunnerRef.h"
#include "MaximizationRunnerRef.h"
#include "LogLikelihoodCalculator.h"

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

        std::vector<double> GenerateRange(double start, double end, double step)
        {

            const int size = int((end - start) / step);
            std::vector<double> result(size);
            std::fill(result.begin(), result.end(), start);

            for (int i = 0; i < size; i++)
            {
                result[i] += step * i;
            }

            return result;
        }

        std::vector<double> GenerateValues(std::vector<double> input, const std::vector<GaussianComponent>& components)
        {
            std::vector<double> output(input.size(), 0.0);
            for (unsigned i = 0; i < input.size(); i++)
            {
                for (unsigned j = 0; j < components.size(); j++)
                {
                    output[i] += components[j].weight * Gaussian(input[i], components[j].mean, components[j].deviation);
                }
            }
            return output;
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

    TEST_F(GaussianMixtureModelTest, test_em)
    {
        std::vector<double> mz = GenerateRange(-20.0, 20.0, 0.5);
        std::vector<double> intensities = GenerateValues(mz, gaussianComponents);
        std::mt19937_64 rngEngine(0);

        ExpectationMaximization<
            BasicInitializationRef, 
            ExpectationRunnerRef, 
            MaximizationRunnerRef,
            LogLikelihoodCalculator
        > em(&mz[0], &intensities[0], (unsigned)mz.size(), rngEngine, 3);

        GaussianMixtureModel result = em.EstimateGmm();
        std::vector<double> approximates = GenerateValues(mz, result.components);

        std::vector<double> errors(intensities.size());
        for (unsigned i = 0; i < intensities.size(); i++)
        {
            errors[i] = pow(intensities[i] - approximates[i], 2) * 10000.0; // scale the errors
        }

        double averageError = 0.0;
        for (unsigned i = 0; i < errors.size(); i++)
        {
            averageError += errors[i];
        }
        averageError /= errors.size();
        double maxError = *std::max_element(errors.begin(), errors.end());
        double minError = *std::min_element(errors.begin(), errors.end());
        printf("Avg error: %f\n", averageError);
        printf("Max error: %f\n", maxError);
        printf("Min error: %f\n", minError);

        EXPECT_LE(averageError, 0.1);

        /*printf("Data:\n");
        for (auto& c : components)
        {
            printf("Weight: %f\n", c.weight);
            printf("Mean: %f\n", c.mean);
            printf("Std: %f\n", c.deviation);
        }

        printf("Result:\n");
        for (auto& c : result.components)
        {
            printf("Weight: %f\n", c.weight);
            printf("Mean: %f\n", c.mean);
            printf("Std: %f\n", c.deviation);
        }*/

    }
}