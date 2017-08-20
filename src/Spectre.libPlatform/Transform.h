/*
* Transform.h
* A more functional approach to std::transform.
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
#include <algorithm>
#include <vector>
#include <span.h>
#include "Spectre.libException/InconsistentArgumentSizesException.h"

namespace Spectre::libPlatform::Functional
{
template< class InputType, class OutputType, class UnaryOperation >
std::vector<OutputType> transform(gsl::span<const InputType> first, UnaryOperation unaryOperation, OutputType* = nullptr)
{
    std::vector<OutputType> result;
    result.reserve(first.size());
    std::transform(first.begin(), first.end(), std::back_inserter(result), unaryOperation);
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
        std::vector<OutputType> result;
        result.reserve(first.size());
        // @gmrukwa: code below is equialent to the following line but causes no warning
        // std::transform(first.begin(), first.end(), second.begin(), std::back_inserter(result), binaryOperation);
        auto iterator1 = first.begin();
        auto iterator2 = second.begin();
        while (iterator1 != first.end())
        {
            result.push_back(binaryOperation(*iterator1++, *iterator2++));
        }
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
}
