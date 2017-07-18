/*
* Generation.h
* Collection of Individuals.
*
Copyright 2017 Grzegorz Mrukwa, Wojciech Wilgierz

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
#include "Individual.h"

namespace Spectre::libGenetic
{
class Generation
{
public:
	explicit Generation(std::vector<Individual>&& generation);
    Generation operator+(const Generation& other) const;
    Generation& operator+=(const Generation& other);
    const Individual& operator[](size_t index) const;
    size_t size() const;
    std::vector<Individual>::const_iterator begin() const;
    std::vector<Individual>::const_iterator end() const;
	virtual ~Generation() = default;

private:
	std::vector<Individual> m_Generation;
};
}
