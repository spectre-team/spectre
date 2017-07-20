/*
* PreservationStrategy.h
* Strategy describing how best individuals are preserved between generations.
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
#include <span.h>
#include "DataTypes.h"
#include "Generation.h"

namespace Spectre::libGenetic
{
/// <summary>
/// Describes how to preserve best individuals between iterations.s
/// </summary>
class PreservationStrategy
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="PreservationStrategy"/> class.
    /// </summary>
    /// <param name="preservationRate">The preservation rate.</param>
    explicit PreservationStrategy(double preservationRate = 0.2);
    /// <summary>
    /// Cleans up an instance of the <see cref="PreservationStrategy"/> class.
    /// </summary>
    virtual ~PreservationStrategy() = default;
    /// <summary>
    /// Picks the best individuals.
    /// </summary>
    /// <param name="generation">The generation.</param>
    /// <param name="scores">The scores.</param>
    /// <returns>Best subpopulation.</returns>
    Generation PickBest(const Generation& generation, gsl::span<ScoreType> scores);
private:
    /// <summary>
    /// Rate of individuals preserved between generations..
    /// </summary>
    const double m_PreservationRate;
};
}
