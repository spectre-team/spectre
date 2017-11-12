/*
* Range.h
* Generates range of numbers.
*
Copyright 2017 Grzegorz Mrukwa

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
#include "ZeroStepException.h"

namespace Spectre::libFunctional
{
/// <summary>
/// Create range of values from lowerBound (inclusively) to upperBound (exclusively) with specified step.
/// </summary>
/// <param name="lowerBound">The lower bound.</param>
/// <param name="upperBound">The upper bound.</param>
/// <param name="step">The step of range.</param>
/// <returns>Vector of consecutive values</returns>
template <class NumericType>
std::vector<NumericType> range(NumericType lowerBound, NumericType upperBound, NumericType step)
{
    static_assert(std::is_arithmetic_v<NumericType>, "NumericType: expected arithmetic.");
    if (step != 0)
    {
        const auto size = static_cast<size_t>((upperBound - lowerBound) / step);
        std::vector<NumericType> result(size);
        for (size_t i = 0; i < size; ++i)
        {
            result[i] = lowerBound + static_cast<NumericType>(i) * step;
        }
        return result;
    }
    else
    {
        throw ZeroStepException();
    }
}

/// <summary>
/// Create range of values from lowerBound (inclusively) to upperBound (exclusively) with step 1.
/// </summary>
/// <param name="lowerBound">The lower bound.</param>
/// <param name="upperBound">The upper bound.</param>
/// <returns>Vector of consecutive values</returns>
template <class NumericType>
std::vector<NumericType> range(NumericType lowerBound, NumericType upperBound)
{
    static_assert(std::is_arithmetic_v<NumericType>, "NumericType: expected arithmetic.");
    return range(lowerBound, upperBound, static_cast<NumericType>(1));
}

/// <summary>
/// Create range of values from zero (inclusively) to upperBound (exclusively) with step 1.
/// </summary>
/// <param name="upperBound">The upper bound.</param>
/// <returns>Vector of consecutive values</returns>
template <class NumericType>
std::vector<NumericType> range(NumericType upperBound)
{
    static_assert(std::is_arithmetic_v<NumericType>, "NumericType: expected arithmetic.");
    return range(static_cast<NumericType>(0), upperBound);
}
}
