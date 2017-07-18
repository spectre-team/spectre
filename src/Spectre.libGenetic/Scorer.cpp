#include <algorithm>
#include "Spectre.libException/NullPointerException.h"
#include "Scorer.h"

namespace Spectre::libGenetic
{
Scorer::Scorer(std::unique_ptr<FitnessFunction> fitnessFunction):
    m_FitnessFunction(std::move(fitnessFunction))
{
    if(m_FitnessFunction != nullptr)
    {
        
    }
    else
    {
        throw libException::NullPointerException("fitnessFunction");
    }
}


std::vector<ScoreType> Scorer::Score(const Generation& generation)
{
    std::vector<ScoreType> scores(generation.size());
    std::transform(generation.begin(), generation.end(), scores.begin(),
        [this](const Individual& individual) { return m_FitnessFunction->operator()(individual); });
    return std::move(scores);
}
}
