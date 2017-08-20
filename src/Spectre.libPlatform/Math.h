#pragma once
#include <vector>
#include <span.h>
#include "Functional.h"

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
constexpr std::vector<DataType> bitwise_and(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left & right; });
}

template< class DataType >
constexpr std::vector<DataType> logical_and(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left && right; });
}

template< class DataType >
constexpr std::vector<DataType> bitwise_or(gsl::span<const DataType> first, gsl::span<const DataType> second)
{
    return Functional::transform(first, second, [](DataType left, DataType right) { return left | right; });
}

template< class DataType >
constexpr std::vector<DataType> logical_or(gsl::span<const DataType> first, gsl::span<const DataType> second)
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
constexpr std::vector<DataType> bitwise_and(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left & second; });
}

template< class DataType >
constexpr std::vector<DataType> logical_and(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left && second; });
}

template< class DataType >
constexpr std::vector<DataType> bitwise_or(gsl::span<const DataType> first, DataType second)
{
    return Functional::transform(first, [second](DataType left) { return left | second; });
}

template< class DataType >
constexpr std::vector<DataType> logical_or(gsl::span<const DataType> first, DataType second)
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
