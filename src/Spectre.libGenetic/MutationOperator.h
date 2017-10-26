/*
* MutationOperator.h
* Mutates an individual.
*
Copyright 2017 Grzegorz Mrukwa, Wojciech Wilgierz

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
#include "DataTypes.h"

namespace Spectre::libGenetic
{
/// <summary>
/// Specifies, how to apply mutation on an individual.
/// </summary>
class MutationOperator
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="MutationOperator"/> class.
    /// </summary>
    /// <param name="mutationRate">The mutation rate.</param>
    /// <param name="bitSwapRate">The bit swap rate, in the case of mutation.</param>
    /// <param name="rngSeed">The RNG seed.</param>
    explicit MutationOperator(double mutationRate, double bitSwapRate, Seed rngSeed = 0, size_t minimalFillup=0, size_t maximalFillup=std::numeric_limits<size_t>::max());
    /// <summary>
    /// Mutates the specified individual.
    /// </summary>
    /// <param name="individual">The individual.</param>
    /// <returns>Mutated individual.</returns>
    virtual Individual operator()(Individual &&individual);
    virtual ~MutationOperator() = default;
private:
    /// <summary>
    /// The mutation rate.
    /// </summary>
    const double m_MutationRate;
    /// <summary>
    /// The bit swap rate.
    /// </summary>
    const double m_BitSwapRate;
    /// <summary>
    /// The random number generator.
    /// </summary>
    RandomNumberGenerator m_RandomNumberGenerator;
    const size_t m_MinimalFillup;
    const size_t m_MaximalFillup;
};
}
