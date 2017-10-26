/*
* Generation.cpp
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

#include <algorithm>
#include <vector>
#include "Spectre.libException/OutOfRangeException.h"
#include "Generation.h"
#include "InconsistentChromosomeLengthException.h"

using namespace std;

namespace Spectre::libGenetic
{
Generation::Generation(std::vector<Individual> &&generation):
    m_Generation(generation)
{
    if (m_Generation.size() > 0)
    {
        const auto minmax = std::minmax_element(m_Generation.begin(), m_Generation.end(), [](const auto &first, const auto &second) { return first.size() < second.size(); });
        const auto minLength = minmax.first->size();
        const auto maxLength = minmax.second->size();
        if (minLength == maxLength) { }
        else
        {
            throw InconsistentChromosomeLengthException(minLength, maxLength);
        }
    }
    else { }
}

Generation::Generation(size_t size, size_t individualSize, size_t initialFillup)
{
    if (initialFillup > individualSize)
    {
        throw libException::ArgumentOutOfRangeException<size_t>("initialFillup", 0, individualSize, initialFillup);
    }
    for (auto i = 0; i < size; i++)
    {
        Individual individual(individualSize, initialFillup);
        m_Generation.push_back(individual);
    }
}

Generation Generation::operator+(const Generation &other) const
{
    if (size() == 0
        || other.size() == 0
        || m_Generation[0].size() == other.m_Generation[0].size())
    {
        std::vector<Individual> newAllocation;
        newAllocation.reserve(this->m_Generation.size() + other.m_Generation.size());
        newAllocation.insert(newAllocation.end(), this->m_Generation.begin(), this->m_Generation.end());
        newAllocation.insert(newAllocation.end(), other.m_Generation.begin(), other.m_Generation.end());
        Generation generation(std::move(newAllocation));
        return generation;
    }
    else
    {
        throw InconsistentChromosomeLengthException(m_Generation[0].size(), other.m_Generation[0].size());
    }
}

Generation& Generation::operator+=(const Generation &other)
{
    if (m_Generation.size() == 0
        || other.m_Generation.size() == 0
        || m_Generation[0].size() == other.m_Generation.size())
    {
        m_Generation.insert(m_Generation.end(), other.m_Generation.begin(), other.m_Generation.end());
        return *this;
    }
    else
    {
        throw InconsistentChromosomeLengthException(m_Generation[0].size(), other.m_Generation[0].size());
    }
}

size_t Generation::size() const noexcept
{
    return m_Generation.size();
}

const Individual& Generation::operator[](size_t index) const
{
    if (index < m_Generation.size())
    {
        return m_Generation[index];
    }
    else
    {
        throw libException::OutOfRangeException(index, m_Generation.size());
    }
}

Individual& Generation::operator[](size_t index)
{
    if (index < m_Generation.size())
    {
        return m_Generation[index];
    }
    else
    {
        throw libException::OutOfRangeException(index, m_Generation.size());
    }
}


std::vector<Individual>::const_iterator Generation::begin() const
{
    return m_Generation.begin();
}

std::vector<Individual>::const_iterator Generation::end() const
{
    return m_Generation.end();
}
}
