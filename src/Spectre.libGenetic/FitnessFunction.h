/*
* FitnessFunction.h
* Interface of fitness function to be used in genetic algorithm.
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
#include "DataTypes.h"
#include "Individual.h"

namespace Spectre::libGenetic
{
/// <summary>
/// General-purpose interface of a fitness function, scoring the individuals.
/// </summary>
class FitnessFunction
{
public:
    /// <summary>
    /// Scores the specified individual.
    /// </summary>
    /// <param name="individual">The individual.</param>
    /// <returns>Non-negative score, which is greater for more optimal individuals.</returns>
    virtual ScoreType computeFitness(const Individual &individual) = 0;
    virtual ~FitnessFunction() = default;
};
}
