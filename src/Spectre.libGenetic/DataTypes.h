#pragma once
#include <vector>
#include <random>
#include "Spectre.libDataset/IDataset.h"

namespace Spectre::libGenetic
{
class Empty {}; // @gmrukwa: NullObject pattern - hack until we switch off properties of IDataset for void.

using Dataset = Spectre::libDataset::IDataset<std::vector<double>, Empty, Empty>;

using ScoreType = double;
using Individual = std::vector<bool>;
using Generation = std::vector<Individual>;
using RandomNumberGenerator = std::mt19937_64;
using Seed = _ULonglong; // @gmrukwa: from mt19937_64
}
