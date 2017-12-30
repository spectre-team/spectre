/*
* DifferentiatingFeaturesEstimator.h
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

#pragma once
#include <vector>
#include "Spectre.libStatistics/ValuesHomogeneityEstimator.h"
#include "Spectre.libDataset/IDataset.h"

namespace Spectre::libStatistics::statistical_learning
{
/// <summary>
/// Estimates differentiating features.
/// </summary>
class DifferentiatingFeaturesEstimator
{
using ReadonlyMatrix = gsl::span<const Values>;
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="DifferentiatingFeaturesEstimator"/> class.
    /// </summary>
    /// <param name="homogeneityEstimator">The homogeneity estimator.</param>
    explicit DifferentiatingFeaturesEstimator(const statistical_testing::ValuesHomogeneityEstimator &homogeneityEstimator);

    /// <summary>
    /// Finds the differentiating features.
    /// </summary>
    /// <param name="first">The first dataset.</param>
    /// <param name="second">The second dataset.</param>
    /// <returns>Statistical indexes expressing feature differentiation potential.</returns>
    template <class SampleMetadata1, class Metadata1, class SampleMetadata2, class Metadata2>
    std::vector<statistical_testing::StatisticalIndex> Estimate(const libDataset::IDataset<Values, SampleMetadata1, Metadata1> &first,
                                                                const libDataset::IDataset<Values, SampleMetadata2, Metadata2> &second) const
    {
        return Estimate(first.GetData(), second.GetData());
    }

private:
    const statistical_testing::ValuesHomogeneityEstimator &m_homogeneityEstimator;
    std::vector<statistical_testing::StatisticalIndex> Estimate(ReadonlyMatrix first, ReadonlyMatrix second) const;
};
}
