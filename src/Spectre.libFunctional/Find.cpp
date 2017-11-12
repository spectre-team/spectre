/*
* Find.cpp
* Finds nonzero elements.
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

#include "Find.h"

namespace Spectre::libFunctional
{
std::vector<size_t> find(std::vector<bool> collection)
{
    const auto indexRange = range(collection.size());
    std::vector<std::size_t> preservedIndexes;
    std::copy_if(
        indexRange.begin(),
        indexRange.end(),
        std::back_inserter(preservedIndexes),
        [&collection](size_t index) { return collection[index]; });
    return preservedIndexes;
}
}