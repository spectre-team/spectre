/*
* Statistics.h
* Statistics calculated on vectors.
*
Copyright 2017 Grzegorz Mrukwa

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

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
    return Sum(data) / (data.size() != 0 ? data.size() : 1);
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
