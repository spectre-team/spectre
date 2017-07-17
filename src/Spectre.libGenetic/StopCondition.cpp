#include <exception>
#include "StopCondition.h"

namespace Spectre::libGenetic
{
bool StopCondition::operator()()
{
    throw std::exception("Not implemented");
}
}
