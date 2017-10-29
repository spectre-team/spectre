/*
* Scorer.h
* Class scoring each individual in generation with given fitness function.
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
#include <memory>
#include "DataTypes.h"
#include "FitnessFunction.h"
#include "Generation.h"

namespace Spectre::libGenetic
{
/// <summary>
/// Scores the population with given fitness function.
/// </summary>
class Scorer
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="Scorer"/> class.
    /// </summary>
    /// <param name="">Existing instance.</param>
    Scorer(Scorer &&) = default;
    /// <summary>
    /// Initializes a new instance of the <see cref="Scorer"/> class.
    /// </summary>
    /// <param name="fitnessFunction">The fitness function.</param>
    /// <param name="numberOfCores">The number of cores used in scoring of the generation.</param>
    explicit Scorer(std::unique_ptr<FitnessFunction> fitnessFunction, unsigned int numberOfCores=1u);
    /// <summary>
    /// Scores the specified generation.
    /// </summary>
    /// <param name="generation">The generation.</param>
    /// <returns>Score vector.</returns>
    virtual std::vector<ScoreType> Score(const Generation &generation);
    virtual ~Scorer() = default;
private:
    /// <summary>
    /// The fitness function.
    /// </summary>
    std::unique_ptr<FitnessFunction> m_FitnessFunction;
    /// <summary>
    /// The number of cores used in scoring of the generation.
    /// </summary>
    const unsigned int m_NumberOfCores;
};
}
