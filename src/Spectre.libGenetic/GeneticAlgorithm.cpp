/*
* GeneticAlgorithm.cpp
* Main algorithm class.
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

#include "Generation.h"
#include "GeneticAlgorithm.h"

namespace Spectre::libGenetic
{
GeneticAlgorithm::GeneticAlgorithm(OffspringGenerator&& offspringGenerator, Scorer&& scorer, StopCondition&& stopCondition)
    : m_OffspringGenerator(std::move(offspringGenerator)),
      m_Scorer(std::move(scorer)),
      m_StopCondition(std::move(stopCondition))
{
}

Generation GeneticAlgorithm::evolve(Generation&& generation)
{
    while(!m_StopCondition())
    {
        const auto scores = m_Scorer.Score(generation);
        generation = m_OffspringGenerator.next(generation, scores);
    }
    return generation;
}
}
