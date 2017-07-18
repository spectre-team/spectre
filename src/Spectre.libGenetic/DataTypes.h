#pragma once
#include <random>
#include "Generation.h"

namespace Spectre::libGenetic
{
using ScoreType = double;
using RandomNumberGenerator = std::mt19937_64;
using Seed = _ULonglong; // @gmrukwa: from mt19937_64
}
