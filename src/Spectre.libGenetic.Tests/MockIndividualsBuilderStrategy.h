/*
* MockIndividualBuilderStrategy.h
* Mocks IndividualBuilderStrategy.
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
#include <gmock/gmock.h>
#include <span.h>
#include "Spectre.libGenetic/IndividualsBuilderStrategy.h"

namespace Spectre::libGenetic::Tests
{
class MockIndividualsBuilderStrategy: public IndividualsBuilderStrategy
{
public:
    MockIndividualsBuilderStrategy(): 
        IndividualsBuilderStrategy(std::make_unique<CrossoverOperator>(),
                                   std::make_unique<MutationOperator>(0, 0),
                                   std::make_unique<ParentSelectionStrategy>()) {}
    MOCK_CONST_METHOD3(BuildFunction, Generation(Generation&, std::vector<ScoreType>, size_t));
    Generation Build(Generation& old, gsl::span<const ScoreType> scores, size_t size) const override { return BuildFunction(old, std::vector<ScoreType>(scores.begin(), scores.end()), size); }
};
}
