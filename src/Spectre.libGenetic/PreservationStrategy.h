#pragma once
#include <span.h>
#include "DataTypes.h"
#include "Generation.h"

namespace Spectre::libGenetic
{
class PreservationStrategy
{
public:
	explicit PreservationStrategy(double preservationRate);
	virtual ~PreservationStrategy() = default;
    Generation PickBest(const Generation& generation, gsl::span<ScoreType> scores);
private:
    const double m_PreservationRate;
};
}
