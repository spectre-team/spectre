/*
* MockOffspringGenerator.h
* Mocks OffspringGenerator
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
#include "Spectre.libGenetic/OffspringGenerator.h"
#include "MockIndividualsBuilderStrategy.h"
#include "MockPreservationStrategy.h"

namespace Spectre::libGenetic::Tests
{
class MockOffspringGenerator: public OffspringGenerator
{
public:
    MockOffspringGenerator() :
        OffspringGenerator(std::make_unique<MockIndividualsBuilderStrategy>(),
                           std::make_unique<MockPreservationStrategy>())
    {}
    MOCK_CONST_METHOD2(NextFunction, Generation(Generation&, std::vector<ScoreType>));
    Generation next(Generation& old, gsl::span<const ScoreType> scores) const override
    {
        return NextFunction(old, std::vector<ScoreType>(scores.begin(), scores.end()));
    }
};
}
