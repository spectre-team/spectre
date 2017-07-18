/*
* OffspringGenerator.h
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

#pragma once
#include <span.h>
#include "DataTypes.h"
#include "Generation.h"
#include "IndividualsBuilderStrategy.h"
#include "PreservationStrategy.h"

namespace Spectre::libGenetic
{
class OffspringGenerator
{
public:
    OffspringGenerator(OffspringGenerator&&) = default;
    explicit OffspringGenerator(IndividualsBuilderStrategy&& builder, PreservationStrategy&& selection);
    Generation next(const Generation& old, gsl::span<ScoreType>&& scores);
    virtual ~OffspringGenerator() = default;
private:
    IndividualsBuilderStrategy m_Builder;
    PreservationStrategy m_PreservationStrategy;
};
}
