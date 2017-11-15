/*
* DifferentiatingFeaturesEstimatorTest.cpp
* Tests DifferentiatingFeaturesEstimatorTest class.
*
Copyright 2017 Spectre Team

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
#include "Spectre.libDataset/Dataset.h"
#include "Spectre.libDataset/Empty.h"
#include "Spectre.libException/EmptyDatasetException.h"
#include "Spectre.libStatistics/DifferentiatingFeaturesEstimator.h"
#include "Spectre.libStatistics.Tests/ValuesHomogeneityEstimatorMock.h"
#include "Spectre.libStatistics/InconsistentNumberOfFeaturesException.h"

namespace
{
using namespace testing;
using namespace Spectre::libDataset;
using namespace Spectre::libException;
using namespace Spectre::libStatistics::Tests;
using namespace Spectre::libStatistics;
using namespace statistical_learning;

using SimplestDataset = Dataset<std::vector<PrecisionType>, const Empty*, const Empty*>;

const Empty *justNothing = &Empty::instance();

const std::vector<PrecisionType> row1 { 1., 2., 3. };
const std::vector<PrecisionType> row2 { 4., 5., 6. };
const std::vector<PrecisionType> row3 { 7., 8., 9. };
const std::vector<PrecisionType> narrowRow { 10. };

const std::vector<std::vector<PrecisionType>> first { row1 };
const std::vector<const Empty*> firstMetadata { justNothing };
const std::vector<std::vector<PrecisionType>> second { row2, row3 };
const std::vector<const Empty*> secondMetadata { justNothing, justNothing };
const std::vector<std::vector<PrecisionType>> emptyData;
const std::vector<const Empty*> emptyMetadata;
const std::vector<std::vector<PrecisionType>> narrowData { narrowRow };

const SimplestDataset firstDataset(first, firstMetadata, justNothing);
const SimplestDataset secondDataset(second, secondMetadata, justNothing);
const SimplestDataset empty(emptyData, emptyMetadata, justNothing);
const SimplestDataset narrowDataset(narrowData, firstMetadata, justNothing);

const std::string interpretation = "BLAAAAH";
const statistical_testing::StatisticalIndex index(1.0, 0, interpretation);

TEST(DifferentiatingFeaturesEstimator, initializes)
{
    const ValuesHomogeneityEstimatorMock mock;
    EXPECT_NO_THROW(DifferentiatingFeaturesEstimator estimator(mock));
}

TEST(DifferentiatingFeaturesEstimator, estimates_without_throw_for_valid_datasets)
{
    const ValuesHomogeneityEstimatorMock mock;
    DifferentiatingFeaturesEstimator estimator(mock);
    EXPECT_CALL(mock, Compare(_, _)).WillRepeatedly(Return(index));
    EXPECT_NO_THROW(estimator.Estimate(firstDataset, secondDataset));
}

TEST(DifferentiatingFeaturesEstimator, throws_for_empty_dataset)
{
    const ValuesHomogeneityEstimatorMock mock;
    DifferentiatingFeaturesEstimator estimator(mock);
    EXPECT_THROW(estimator.Estimate(empty, secondDataset), EmptyDatasetException);
    EXPECT_THROW(estimator.Estimate(firstDataset, empty), EmptyDatasetException);
    EXPECT_THROW(estimator.Estimate(empty, empty), EmptyDatasetException);
}

TEST(DifferentiatingFeaturesEstimator, throws_for_inconsistent_number_of_features)
{
    const ValuesHomogeneityEstimatorMock mock;
    DifferentiatingFeaturesEstimator estimator(mock);
    EXPECT_THROW(estimator.Estimate(narrowDataset, secondDataset), InconsistentNumberOfFeaturesException);
}

TEST(DifferentiatingFeaturesEstimator, calls_estimator_for_each_feature)
{
    const ValuesHomogeneityEstimatorMock mock;
    DifferentiatingFeaturesEstimator estimator(mock);
    EXPECT_CALL(mock, Compare(_, _)).Times(static_cast<int>(row1.size())).WillRepeatedly(Return(index));
    estimator.Estimate(firstDataset, secondDataset);
}

TEST(DifferentiatingFeaturesEstimator, returns_index_values_for_each_feature)
{
    const ValuesHomogeneityEstimatorMock mock;
    DifferentiatingFeaturesEstimator estimator(mock);
    EXPECT_CALL(mock, Compare(_, _)).WillRepeatedly(Return(index));
    EXPECT_THAT(estimator.Estimate(firstDataset, secondDataset).size(), Eq(row1.size()));
}
}
