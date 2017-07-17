#include "StopCondition.h"

namespace Spectre::libGenetic
{
StopCondition::StopCondition(unsigned int numberOfIterations):
    m_RemainingIterations(numberOfIterations)
{
    
}


bool StopCondition::operator()()
{
    return m_RemainingIterations-- > 0;
}
}
