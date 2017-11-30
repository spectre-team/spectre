/*
* MockFitnessFunction.h
* Mock for a fitness function scoring the individuals.
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
#include "Spectre.libGenetic/FitnessFunction.h"

namespace Spectre::libGenetic::Tests
{
class MockFitnessFunction: public FitnessFunction
{
public:
    // @gmrukwa: call operator mocked as this
    // https://groups.google.com/forum/#!topic/googlemock/O-5cTVVtswE
    MOCK_CONST_METHOD1(CallOperator, ScoreType(const Individual&));

    ScoreType computeFitness(const Individual &individual) override
    {
        return CallOperator(individual);
    }
};
}
