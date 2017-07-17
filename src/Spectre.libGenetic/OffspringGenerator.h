#pragma once
#include "Generation.h"
#include <packages/Microsoft.Gsl.0.1.2.1/build/native/include/span.h>

namespace Spectre::libGenetic
{
class OffspringGenerator
{
public:
    OffspringGenerator();
    Generation next(Generation old, gsl::span<double> scores);
    virtual ~OffspringGenerator() = default;
};
}
