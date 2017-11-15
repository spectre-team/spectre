/*
* DifferentiatingFeaturesEstimator.cpp
* Estimates differentiating features.
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

#include "Spectre.libException/EmptyDatasetException.h"
#include "Spectre.libFunctional/Transform.h"
#include "Spectre.libStatistics/InconsistentNumberOfFeaturesException.h"
#include "Spectre.libStatistics/DifferentiatingFeaturesEstimator.h"

namespace Spectre::libStatistics::statistical_learning
{
DifferentiatingFeaturesEstimator::DifferentiatingFeaturesEstimator(const statistical_testing::ValuesHomogeneityEstimator &homogeneityEstimator) :
    m_homogeneityEstimator(homogeneityEstimator) { }

std::vector<statistical_testing::StatisticalIndex> DifferentiatingFeaturesEstimator::Estimate(ReadonlyMatrix first,
                                                                                              ReadonlyMatrix second) const
{
    if (first.empty())
    {
        throw libException::EmptyDatasetException("first");
    }
    if (second.empty())
    {
        throw libException::EmptyDatasetException("second");
    }
    if (first[0].size() != second[0].size())
    {
        throw InconsistentNumberOfFeaturesException(first[0].size(), second[0].size());
    }
    using namespace libFunctional;
    std::vector<statistical_testing::StatisticalIndex> indexes;
    indexes.reserve(first[0].size());
    for (auto featureNumber = 0u; featureNumber < first[0].size(); ++featureNumber)
    {
        const auto selectIth = [featureNumber](const auto &observation) { return observation[featureNumber]; };
        const auto featureOfFirst = transform(first, selectIth, static_cast<PrecisionType*>(nullptr));
        const auto featureOfSecond = transform(second, selectIth, static_cast<PrecisionType*>(nullptr));
        const auto index = m_homogeneityEstimator.Compare(featureOfFirst, featureOfSecond);
        indexes.push_back(index);
    }
    return indexes;
}
}
