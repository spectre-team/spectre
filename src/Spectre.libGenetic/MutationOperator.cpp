/*
* MutationOperator.cpp
* Mutates an individual.
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

#include <algorithm>
#include "Spectre.libException/ArgumentOutOfRangeException.h"
#include "MutationOperator.h"
#include "Spectre.libGenetic/InconsistentMinimalAndMaximalFillupException.h"

namespace Spectre::libGenetic
{
MutationOperator::MutationOperator(double mutationRate, double bitSwapRate, Seed rngSeed, size_t minimalFillup, size_t maximalFillup):
    m_MutationRate(mutationRate),
    m_BitSwapRate(bitSwapRate),
    m_RandomNumberGenerator(rngSeed),
    m_MinimalFillup(minimalFillup),
    m_MaximalFillup(maximalFillup)
{
    if (m_MutationRate >= 0 && m_MutationRate <= 1) { }
    else
    {
        throw libException::ArgumentOutOfRangeException<double>("mutationRate", 0, 1, m_MutationRate);
    }
    if (m_BitSwapRate >= 0 && m_BitSwapRate <= 1) { }
    else
    {
        throw libException::ArgumentOutOfRangeException<double>("bitSwapRate", 0, 1, m_BitSwapRate);
    }
    if (m_MinimalFillup > m_MaximalFillup)
    {
        throw InconsistentMinimalAndMaximalFillupException(m_MinimalFillup, m_MaximalFillup);
    }
}

Individual MutationOperator::operator()(Individual &&individual)
{
    auto original(individual);

    std::bernoulli_distribution mutationProbability(m_MutationRate);
    if (mutationProbability(m_RandomNumberGenerator))
    {
        std::bernoulli_distribution swapProbability(m_BitSwapRate);
        const auto swap_random_bits = [&swapProbability, this](bool bit)
        {
            return swapProbability(m_RandomNumberGenerator) ? !bit : bit;
        };
        std::transform(individual.begin(), individual.end(), individual.begin(), swap_random_bits);
    }

    auto fillup = 0u;
    for (auto bit : individual)
    {
        if (bit)
        {
            ++fillup;
        }
    }

    if (fillup >= m_MinimalFillup && fillup <= m_MaximalFillup)
    {
        return individual;
    }
    else
    {
        return original;
    }
}
}
