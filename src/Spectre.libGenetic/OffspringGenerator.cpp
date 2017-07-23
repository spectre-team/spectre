/*
* OffspringGenerator.cpp
* Describes how to create offspring from best and new individuals.
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

#include "OffspringGenerator.h"

namespace Spectre::libGenetic
{
OffspringGenerator::OffspringGenerator(IndividualsBuilderStrategy&& builder, PreservationStrategy&& preservationStrategy):
    m_Builder(builder),
    m_PreservationStrategy(preservationStrategy)
{
}

Generation OffspringGenerator::next(Generation& old, gsl::span<const ScoreType>&& scores)
{
    const auto preserved = m_PreservationStrategy.PickBest(old, scores);
    const auto numberOfRemaining = old.size() - preserved.size();
    const auto generated = m_Builder.Build(old, scores, numberOfRemaining);
    return preserved + generated;
}
}
