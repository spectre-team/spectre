/*
* GeneticAlgorithm.h
* Main algorithm class.
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
#include <memory>
#include "Generation.h"
#include "OffspringGenerator.h"
#include "Scorer.h"
#include "StopCondition.h"

namespace Spectre::libGenetic
{
/// <summary>
/// General-purpose genetic algorithm.
/// </summary>
class GeneticAlgorithm
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="GeneticAlgorithm"/> class.
    /// </summary>
    /// <param name="offspringGenerator">The offspring generator.</param>
    /// <param name="scorer">The scorer.</param>
    /// <param name="stopCondition">The stop condition.</param>
    GeneticAlgorithm(std::unique_ptr<OffspringGenerator> offspringGenerator, std::unique_ptr<Scorer> scorer, std::unique_ptr<StopCondition> stopCondition);
    /// <summary>
    /// Evolves the specified generation.
    /// </summary>
    /// <param name="generation">The generation.</param>
    /// <returns>Next, evolved generation.</returns>
    Generation evolve(Generation &&generation) const;
    virtual ~GeneticAlgorithm() = default;

private:
    /// <summary>
    /// The offspring generator.
    /// </summary>
    std::unique_ptr<OffspringGenerator> m_OffspringGenerator;
    /// <summary>
    /// The scorer.
    /// </summary>
    std::unique_ptr<Scorer> m_Scorer;
    /// <summary>
    /// The stop condition.
    /// </summary>
    std::unique_ptr<StopCondition> m_StopCondition;
};
}
