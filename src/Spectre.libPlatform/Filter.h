/*
* Filter.h
* Filters content of the iterable.
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
#include "Spectre.libException/InconsistentArgumentSizesException.h"
#include "Spectre.libException/OutOfRangeException.h"
#include "Find.h"
#include "Transform.h"

namespace Spectre::libPlatform::Functional
{
template <typename DataType>
std::vector<DataType> filter(gsl::span<const DataType> collection, gsl::span<const size_t> indexes)
{
    return transform(
        indexes,
        [&collection](size_t index)
        {
            if (index < collection.size())
            {
                return collection[index];
            }
            else
            {
                throw libException::OutOfRangeException(index, collection.size());
            }
        },
        static_cast<DataType*>(nullptr));
}

template <typename DataType>
std::vector<DataType> filter(gsl::span<const DataType> collection, gsl::span<const bool> isIncluded)
{
    if(collection.size() == isIncluded.size())
    {
        const auto preservedIndexes = find(isIncluded);
        return filter(collection, preservedIndexes);
    }
    else
    {
        throw libException::InconsistentArgumentSizesException(
            "collection",
            collection.size(),
            "isIncluded",
            isIncluded.size());
    }
}
}
