/*
* ParentSelectionStrategy.cpp
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

#include "ParentSelectionStrategy.h"

namespace Spectre::libGenetic
{
ParentSelectionStrategy::ParentSelectionStrategy(Seed seed):
    m_RandomNumberGenerator(seed)
{
    
}

std::pair<const Individual&, const Individual&> ParentSelectionStrategy::next(const Generation& generation, gsl::span<ScoreType> scores)
{
    std::discrete_distribution<> indexDistribution(scores.begin(), scores.end());
    const auto first = indexDistribution(m_RandomNumberGenerator);
    const auto second = indexDistribution(m_RandomNumberGenerator);
    return std::make_pair(generation[first], generation[second]);
}
}
