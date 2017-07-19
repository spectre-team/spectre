/*
* Sorting.h
* Sorting utilities.
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
#include <span.h>
#include <vector>

namespace Spectre::libGenetic
{
/// <summary>
/// Utilities for sorting.
/// </summary>
class Sorting
{
public:
    /// <summary>
    /// Gets the indiceses sorted by the specified data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>Indices</returns>
    template <class T>
    static std::vector<size_t> indices(gsl::span<T> data)
    {
        std::vector<size_t> indices(data.size());
        std::iota(indices.begin(), indices.end(), 0);
        std::sort(indices.begin(), indices.end(), [&data](size_t first, size_t second) { return data[second] < data[first]; });
        return indices;
    }
};
}
