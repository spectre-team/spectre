#pragma once
#include <gmock/gmock.h>
#include "Spectre.libGenetic/FitnessFunction.h"

namespace Spectre::libGenetic::Tests
{
class MockFitnessFunction: public FitnessFunction
{
public:
    // @gmrukwa: unary mocked as this
    // https://groups.google.com/forum/#!topic/googlemock/O-5cTVVtswE
    MOCK_CONST_METHOD1(CalculateFitness, ScoreType(const Individual&));
    ScoreType operator()(const Individual& individual) override
    {
        return CalculateFitness(individual);
    }
};
}
