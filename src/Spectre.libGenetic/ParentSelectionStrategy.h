/*
* ParentSelectionStrategy.h
* Strategy describing how parents are chosen to create offspring.
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
#include "Individual.h"
#include "Generation.h"

namespace Spectre::libGenetic
{
template <typename T>
using reference_pair = std::pair<std::reference_wrapper<T>, std::reference_wrapper<T>>;

/// <summary>
/// Specifies, how to choose parents to crossover
/// </summary>
class ParentSelectionStrategy
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="ParentSelectionStrategy"/> class.
    /// </summary>
    /// <param name="seed">The random number generator seed.</param>
    explicit ParentSelectionStrategy(Seed seed = 0);
    /// <summary>
    /// Returns next pair of parents from current generation.
    /// </summary>
    /// <param name="generation">The generation.</param>
    /// <param name="scores">The scores of individuals.</param>
    /// <returns>Parents for crossover.</returns>
    virtual reference_pair<Individual> next(Generation &generation, gsl::span<const ScoreType> scores);
    virtual ~ParentSelectionStrategy() = default;
private:
    /// <summary>
    /// The random number generator.
    /// </summary>
    RandomNumberGenerator m_RandomNumberGenerator;
};
}
