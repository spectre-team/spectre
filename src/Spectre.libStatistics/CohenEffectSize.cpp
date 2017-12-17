/*
* CohenEffectSize.cpp
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

#include "Spectre.libException/ReachedUnreachableCodeException.h"
#include "Spectre.libStatistics/Statistics.h"
#include "Spectre.libStatistics/Math.h"
#include "CohenEffectSize.h"
#include "Spectre.libException/EmptyArgumentException.h"

namespace Spectre::libStatistics::statistical_testing
{
const std::array<std::string, 5> CohenEffectSize::interpretations = {
    std::string("None"),
    "Small",
    "Medium",
    "Large",
    "Very Large"
};

const std::array<PrecisionType, 5> CohenEffectSize::thresholds = {
    -std::numeric_limits<PrecisionType>::infinity(),
    0.3f,
    0.5f,
    0.8f,
    1.2f
};

using namespace simple_statistics;
using namespace basic_math;

StatisticalIndex CohenEffectSize::Compare(Values first, Values second) const
{
    if (first.empty())
    {
        throw libException::EmptyArgumentException("first");
    }
    if (second.empty())
    {
        throw libException::EmptyArgumentException("second");
    }
    if (first.size() + second.size() == 2)
    {
        return StatisticalIndex(0.0, 0u, interpretations[0]);
    }
    const auto firstMean = Mean(first);
    const auto secondMean = Mean(second);
    const auto std = sqrt(Variance(first, second));
    const auto DCohen = std::abs(firstMean - secondMean) / std;
    for (auto thresholdNumber = thresholds.size() - 1; thresholdNumber >= 0; --thresholdNumber)
    {
        if (DCohen >= thresholds[thresholdNumber])
        {
            return StatisticalIndex(DCohen, static_cast<unsigned>(thresholdNumber), interpretations[thresholdNumber]);
        }
    }
    throw libException::ReachedUnreachableCodeException();
}
}
