/*
* Scorer.cpp
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

#include <algorithm>
#include "Spectre.libException/NullPointerException.h"
#include "Scorer.h"

namespace Spectre::libGenetic
{
Scorer::Scorer(std::unique_ptr<FitnessFunction> fitnessFunction):
    m_FitnessFunction(std::move(fitnessFunction))
{
    if (m_FitnessFunction != nullptr)
    {
        // @gmrukwa: usual empty execution branch
    }
    else
    {
        throw libException::NullPointerException("fitnessFunction");
    }
}

std::vector<ScoreType> Scorer::Score(const Generation &generation)
{
    std::vector<ScoreType> scores(generation.size());
    std::transform(generation.begin(), generation.end(), scores.begin(),
                   [this](const Individual &individual) { return m_FitnessFunction->fit(individual); });
    return std::move(scores);
}
}
