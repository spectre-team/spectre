/*
* GeneticAlgorithmFactory.h
* Builds different versions of GeneticAlgorithm
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
#include <memory>
#include "Spectre.libGenetic/GeneticAlgorithm.h"

namespace Spectre::libGenetic
{
/// <summary>
/// Factory for creating Genetic Algorithm objects with given parameters.
/// </summary>
class GeneticAlgorithmFactory
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="GeneticAlgorithmFactory"/> class.
    /// </summary>
    /// <param name="mutationRate">The mutation rate.</param>
    /// <param name="bitSwapRate">The bit swap rate.</param>
    /// <param name="preservationRate">The preservation rate.</param>
    /// <param name="generationsNumber">The number of generation.</param>
    /// <param name="numberOfCores">The number of cores.</param>
    /// <param name="minimalFillup">The minimal fillup.</param>
    /// <param name="maximalFillup">The maximal fillup.</param>
    GeneticAlgorithmFactory(double mutationRate,
                            double bitSwapRate,
                            double preservationRate,
                            unsigned generationsNumber,
                            unsigned numberOfCores,
                            size_t minimalFillup,
                            size_t maximalFillup);
    /// <summary>
    /// Creates Genetic Algorithm with default parameter values.
    /// </summary>
    /// <param name="fitnessFunction">The fitness function.</param>
    /// <param name="seed">The seed.</param>
    /// <returns>The Genetic Algorithm object</returns>
    std::unique_ptr<GeneticAlgorithm> BuildDefault(std::unique_ptr<FitnessFunction> fitnessFunction,
                                                   Seed seed=0) const;
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
    /// The preservation rate.
    /// </summary>
    const double m_PreservationRate;
    /// <summary>
    /// The number of generations.
    /// </summary>
    const unsigned m_GenerationsNumber;
    /// <summary>
    /// The number of cores.
    /// </summary>
    const unsigned m_NumberOfCores;
    /// <summary>
    /// The minimal fillup.
    /// </summary>
    const size_t m_MinimalFillup;
    /// <summary>
    /// The maximal fillup.
    /// </summary>
    const size_t m_MaximalFillup;
};
}
