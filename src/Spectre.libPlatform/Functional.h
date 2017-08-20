#pragma once
#include <algorithm>
#include <vector>
#include <span.h>
#include "Spectre.libException/InconsistentArgumentSizesException.h"

namespace Spectre::libPlatform::Functional
{

template< class InputType, class OutputType, class UnaryOperation >
std::vector<OutputType> transform(gsl::span<const InputType> first, UnaryOperation unaryOperation, OutputType* = nullptr)
{
    std::vector<OutputType> result(first.size());
    std::transform(first.begin(), first.end(), result.begin(), unaryOperation);
    return result;
}

template< class DataType, class UnaryOperation >
std::vector<DataType> transform(gsl::span<const DataType> first, UnaryOperation unaryOperation)
{
    return transform<DataType, DataType, UnaryOperation>(first, unaryOperation);
}

template< class InputType1, class InputType2, class OutputType, class BinaryOperation >
std::vector<OutputType> transform(gsl::span<const InputType1> first, gsl::span<const InputType2> second, BinaryOperation binaryOperation, OutputType* = nullptr)
{
    if (first.size() == second.size())
    {
        std::vector<OutputType> result(first.size());
        std::transform(first.begin(), first.end(), second.begin(), result.begin(), binaryOperation);
        return result;
    }
    else
    {
        throw libException::InconsistentArgumentSizesException("first", first.size(), "second", second.size());
    }
}

template< class DataType, class BinaryOperation >
std::vector<DataType> transform(gsl::span<const DataType> first, gsl::span<const DataType> second, BinaryOperation binaryOperation)
{
    return transform<DataType, DataType, DataType, BinaryOperation>(first, second, binaryOperation);
}

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
