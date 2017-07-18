/*
* Individual.h
* Binary individual class.
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

namespace Spectre::libGenetic
{
class Individual
{
public:
	explicit Individual(std::vector<bool>&& binaryData);
    std::vector<bool>::reference operator[](size_t index);
    std::vector<bool>::iterator begin();
    std::vector<bool>::iterator end();
    std::vector<bool>::const_iterator begin() const;
    std::vector<bool>::const_iterator end() const;
    size_t size() const;
	virtual ~Individual() = default;

private:
	std::vector<bool> m_BinaryData;
};
}
