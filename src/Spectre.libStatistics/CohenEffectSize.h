/*
* CohenEffectSize.h
* Estimates effect size.
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

#include "ValuesHomogeneityEstimator.h"

namespace Spectre::libStatistics::statistical_testing
{
/// <summary>
/// Estimates effect size.
/// </summary>
class CohenEffectSize final : public ValuesHomogeneityEstimator
{
public:
    /// <summary>
    /// Estimates effect size of difference of mean in two sets.
    /// </summary>
    /// <param name="first">First set.</param>
    /// <param name="second">Second set.</param>
    /// <returns>Cohen's D statistic.</returns>
    StatisticalIndex Compare(Values first, Values second) const override;
private:
    static const std::array<std::string, 5> interpretations;
    static const std::array<PrecisionType, 5> thresholds;
};
}
