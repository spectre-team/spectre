#pragma once
#include "ExceptionBase.h"

namespace Spectre::libException
{
template <typename DataType>
class ArgumentOutOfRangeException: public ExceptionBase
{
public:
    ArgumentOutOfRangeException(const std::string& variableName,
                                DataType lowerBound,
                                DataType upperBound,
                                DataType actual):
        ExceptionBase(variableName + " ranged (" + std::to_string(lowerBound) + ", "
            + std::to_string(upperBound) + ") provided with " + std::to_string(actual))
    {
        
    }
};
}