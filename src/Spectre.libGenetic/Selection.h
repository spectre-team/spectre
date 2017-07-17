#pragma once
#include "DataTypes.h"
#include "Generation.h"

namespace Spectre::libGenetic
{
class Selection
{
public:
	Selection() = default;
	virtual ~Selection() = default;
	Generation performSelection(Generation generation, long size);
	Generation performSelection(Generation generation, long size, long includedFrom = 100, long includedTo = 100);
	Generation performSelection(const Dataset* data, long size, long includedFrom = 100, long includedTo = 100);
};
}
