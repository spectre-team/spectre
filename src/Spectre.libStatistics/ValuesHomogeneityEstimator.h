/*
* ValuesHomogeneityEstimator.h
* Compares level of values in two sets.
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
#include <span.h>
#include "Spectre.libStatistics/StatisticalIndex.h"

namespace Spectre::libStatistics::statistical_testing
{
/// <summary>
/// Compares level of values in two sets.
/// </summary>
class ValuesHomogeneityEstimator
{
public:
    /// <summary>
    /// Compares level homogeneity in two sets.
    /// </summary>
    /// <param name="first">First set.</param>
    /// <param name="second">Second set.</param>
    virtual StatisticalIndex Compare(gsl::span<const PrecisionType> first, gsl::span<const PrecisionType> second) const = 0;

    /// <summary>
    /// Finalizes an instance of the <see cref="ValuesHomogeneityEstimator"/> class.
    /// </summary>
    virtual ~ValuesHomogeneityEstimator() = default;
};
}
