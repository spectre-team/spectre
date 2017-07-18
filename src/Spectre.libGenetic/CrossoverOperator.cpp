/*
* CrossoverOperator.cpp
* Generates new Individual from two parents.
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

#include "CrossoverOperator.h"
#include "Individual.h"

namespace Spectre::libGenetic
{
CrossoverOperator::CrossoverOperator(Seed rngSeed):
    m_RandomNumberGenerator(rngSeed)
{
}

Individual CrossoverOperator::operator()(const Individual& first, const Individual& second)
{
    std::uniform_int_distribution<size_t> distribution(0, first.size());
    const auto cuttingPoint = distribution(m_RandomNumberGenerator);
    const auto endOfFirst = first.begin() + cuttingPoint;
    const auto beginningOfSecond = second.begin() + cuttingPoint;
    std::vector<bool> phenotype;
    phenotype.reserve(first.size());
    phenotype.insert(phenotype.end(), first.begin(), endOfFirst);
    phenotype.insert(phenotype.end(), beginningOfSecond, second.end());
    Individual individual(std::move(phenotype));
    return std::move(individual);
}
}
