/*
* StatisticalIndex.h
* Represents a statistical index that has information about its strength.
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
#include <string>
#include "Types.h"

namespace Spectre::libStatistics::statistical_testing
{
/// <summary>
/// Represents a statistical index that has information about its strength.
/// </summary>
class StatisticalIndex final
{
public:
    /// <summary>
    /// Value of the index.
    /// </summary>
    const PrecisionType value;
    /// <summary>
    /// Strength of the index value. The higher the better.
    /// </summary>
    const unsigned strength;
    /// <summary>
    /// Human-readable interpretation of the index value.
    /// </summary>
    const std::string &interpretation;
    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticalIndex"/> class.
    /// </summary>
    /// <param name="value">The value of the index.</param>
    /// <param name="strength">The strength of the index.</param>
    StatisticalIndex(PrecisionType value, unsigned strength, const std::string &interpretation);
};
}
