#pragma once
#include <vector>
#include <span.h>

namespace Spectre::libPlatform::Functional
{
template <class NumericType>
std::vector<NumericType> range(NumericType lowerBound, NumericType upperBound, NumericType step)
{
    std::vector<NumericType> result(upperBound);
    for (NumericType i = lowerBound; i < upperBound; i += step)
    {
        result[i] = i;
    }
    return result;
}

template <class NumericType>
std::vector<NumericType> range(NumericType lowerBound, NumericType upperBound)
{
    return range(lowerBound, upperBound, static_cast<NumericType>(1));
}

template <class NumericType>
std::vector<NumericType> range(NumericType upperBound)
{
    return range(static_cast<NumericType>(0), upperBound);
}
}
