#pragma once
#include "DataTypes.h"
#include "Generation.h"
#include "Scorer.h"
#include "OffspringGenerator.h"
#include "StopCondition.h"

namespace Spectre::libGenetic
{
class GeneticAlgorithm
{
public:
	GeneticAlgorithm(OffspringGenerator&& offspringGenerator, Scorer&& classifier, StopCondition&& stopCondition);
    Generation evolve(Generation&& generation);
	virtual ~GeneticAlgorithm() = default;

private:
    OffspringGenerator m_OffspringGenerator;
	Scorer m_Scorer;
    StopCondition m_StopCondition;
};
}
