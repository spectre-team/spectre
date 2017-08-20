/*
* Math.h
* Basic mathematical operations on vectors.
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
#include <vector>
#include <span.h>
#include "Transform.h"

namespace Spectre::libPlatform::Math
{
// @gmrukwa: vector vs vector

template< class DataType >
constexpr std::vector<DataType> plus(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left + right; });
}

template< class DataType >
constexpr std::vector<DataType> minus(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left - right; });
}

template< class DataType >
constexpr std::vector<DataType> times(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left * right; });
}

template< class DataType >
constexpr std::vector<DataType> divideBy(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left / right; });
}

template< class DataType >
constexpr std::vector<DataType> modulo(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left % right; });
}

template< class DataType >
constexpr std::vector<DataType> bitwiseAnd(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left & right; });
}

template< class DataType >
constexpr std::vector<DataType> logicalAnd(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left && right; });
}

template< class DataType >
constexpr std::vector<DataType> bitwiseOr(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left | right; });
}

template< class DataType >
constexpr std::vector<DataType> logicalOr(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left || right; });
}

template<class DataType>
constexpr std::vector<DataType> max(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return std::max(left, right); });
}

template<class DataType>
constexpr std::vector<DataType> min(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return std::min(left, right); });
}

// @gmrukwa: vector vs scalar

template< class DataType >
constexpr std::vector<DataType> plus(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left + second; });
}

template< class DataType >
constexpr std::vector<DataType> minus(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left - second; });
}

template< class DataType >
constexpr std::vector<DataType> times(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left * second; });
}

template< class DataType >
constexpr std::vector<DataType> divideBy(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left / second; });
}

template< class DataType >
constexpr std::vector<DataType> modulo(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left % second; });
}

template< class DataType >
constexpr std::vector<DataType> bitwiseAnd(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left & second; });
}

template< class DataType >
constexpr std::vector<DataType> logicalAnd(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left && second; });
}

template< class DataType >
constexpr std::vector<DataType> bitwiseOr(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left | second; });
}

template< class DataType >
constexpr std::vector<DataType> logicalOr(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left || second; });
}

template<class DataType>
constexpr std::vector<DataType> max(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return std::max(left, second); });
}

template<class DataType>
constexpr std::vector<DataType> min(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return std::min(left, second); });
}

// @gmrukwa: unary

template <class DataType>
constexpr std::vector<DataType> abs(gsl::span<const DataType> data)
{
    return Functional::transform(data, [](DataType entry) { return std::abs(entry); });
}
}
