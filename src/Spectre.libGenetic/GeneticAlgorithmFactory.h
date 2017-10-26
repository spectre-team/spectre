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
class GeneticAlgorithmFactory
{
public:
    GeneticAlgorithmFactory(double mutationRate,
                            double bitSwapRate,
                            double preservationRate,
                            unsigned generationsNumber,
                            unsigned numberOfCores);
    std::unique_ptr<GeneticAlgorithm> BuildDefault(std::unique_ptr<FitnessFunction> fitnessFunction,
                                                   Seed seed=0) const;
private:
    const double m_MutationRate;
    const double m_BitSwapRate;
    const double m_PreservationRate;
    const unsigned m_GenerationsNumber;
    const unsigned m_NumberOfCores;
};
}
