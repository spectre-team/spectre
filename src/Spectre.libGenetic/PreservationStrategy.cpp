/*
* PreservationStrategy.cpp
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

#include "PreservationStrategy.h"
#include "Sorting.h"

namespace Spectre::libGenetic
{
PreservationStrategy::PreservationStrategy(double preservationRate):
    m_PreservationRate(preservationRate)
{
}

Generation PreservationStrategy::PickBest(const Generation& generation, gsl::span<ScoreType> scores)
{
    const auto indices = Sorting::indices(scores);
    const auto numberOfPreserved = static_cast<size_t>(std::min(m_PreservationRate * generation.size(), 1.0));
    std::vector<Individual> bestIndividuals;
    bestIndividuals.reserve(numberOfPreserved);
    std::transform(indices.begin(), indices.begin() + numberOfPreserved, std::back_inserter(bestIndividuals), [&generation](size_t index) { return generation[index]; });
    Generation newGeneration(std::move(bestIndividuals));
    return newGeneration;
}

}
