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
#include "Spectre.libException/ArgumentOutOfRangeException.h"
#include "InconsistentGenerationAndScoresLengthException.h"

namespace Spectre::libGenetic
{
ParentSelectionStrategy::ParentSelectionStrategy(Seed seed):
    m_RandomNumberGenerator(seed)
{
    
}

std::pair<const Individual&, const Individual&> ParentSelectionStrategy::next(const Generation& generation, gsl::span<const ScoreType> scores)
{
    if (generation.size() == static_cast<size_t>(scores.size()))
    {
        // @gmrukwa: usual empty execution branch
    }
    else
    {
        throw InconsistentGenerationAndScoresLengthException(generation.size(), scores.size());
    }
    const auto minAndMaxWeights = std::minmax_element(scores.begin(), scores.end());
    const auto minWeight = *minAndMaxWeights.first;
    const auto maxWeight = *minAndMaxWeights.second;
    if (minWeight >= 0)
    {
        // @gmrukwa: usual empty execution branch
    }
    else
    {
        throw libException::ArgumentOutOfRangeException<ScoreType>("scores", 0, std::numeric_limits<ScoreType>::max(), minWeight);
    }
    std::vector<ScoreType> defaultScores(scores.size(), 1);
    if (maxWeight > 0)
    {
        // @gmrukwa: usual empty execution branch
    }
    else
    {
        scores = defaultScores;
    }
    std::discrete_distribution<> indexDistribution(scores.begin(), scores.end());
    const auto first = indexDistribution(m_RandomNumberGenerator);
    const auto second = indexDistribution(m_RandomNumberGenerator);
    return std::make_pair(generation[first], generation[second]);
}
}
