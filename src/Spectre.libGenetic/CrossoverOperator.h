/*
* CrossoverOperator.h
* Generates new Individual from two parents.
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
/// Performs crossover between parent chromosomes and produces a child one.
/// </summary>
class CrossoverOperator
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="CrossoverOperator"/> class.
    /// </summary>
    /// <param name="rngSeed">The RNG seed.</param>
    explicit CrossoverOperator(Seed rngSeed = 0, size_t minimalFillup=0, size_t maximalFillup=std::numeric_limits<size_t>::max());
    virtual ~CrossoverOperator() = default;
    /// <summary>
    /// Create new individual.
    /// </summary>
    /// <param name="first">The first parent.</param>
    /// <param name="second">The second parent.</param>
    /// <returns>A child.</returns>
    virtual Individual operator()(const Individual &first, const Individual &second);

private:
    RandomNumberGenerator m_RandomNumberGenerator;
    const size_t m_MinimalFillup;
    const size_t m_MaximalFillup;
};
}
