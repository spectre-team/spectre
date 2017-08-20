#pragma once
#include <numeric>
#include <span.h>
#include "Math.h"

namespace Spectre::libPlatform::Statistics
{
template <class DataType>
constexpr DataType Sum(gsl::span<const DataType> data)
{
    return std::accumulate(data.begin(), data.end(), static_cast<DataType>(0));
}

template <class DataType>
constexpr DataType Mean(gsl::span<const DataType> data)
{
    return Sum(data) / data.size();
}

template <class DataType>
DataType MeanAbsoluteDeviation(gsl::span<const DataType> data)
{
    const auto mean = Mean(data);
    const auto deviation = Math::minus(data, mean);
    const auto absoluteDeviation = Math::abs(gsl::as_span(deviation));
    return Mean(gsl::as_span(absoluteDeviation));
}
}
