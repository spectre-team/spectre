/*
* Individual.cpp
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

#include <vector>
#include <algorithm>
#include "Spectre.libException/OutOfRangeException.h"
#include "Individual.h"
#include "InconsistentChromosomeLengthException.h"

using namespace std;

namespace Spectre::libGenetic
{
Individual::Individual(std::vector<bool> &&binaryData):
    m_BinaryData(binaryData) { }


Individual::Individual(size_t size, size_t initialFillup, Seed seed)
{
    if (initialFillup > size)
    {
        throw libException::ArgumentOutOfRangeException<size_t>("initialFillup", 0, size, initialFillup);
    }
    for (auto i = 0; i < initialFillup; i++)
    {
        m_BinaryData.push_back(true);
    }
    for (auto i = initialFillup; i < size; i++)
    {
        m_BinaryData.push_back(false);
    }
    RandomNumberGenerator rng(seed);
    std::shuffle(m_BinaryData.begin(), m_BinaryData.end(), rng);
}

std::vector<bool> Individual::getData() const
{
    return m_BinaryData;
}

std::vector<bool>::reference Individual::operator[](size_t index)
{
    if (index < m_BinaryData.size())
    {
        return m_BinaryData[index];
    }
    else
    {
        throw libException::OutOfRangeException(index, m_BinaryData.size());
    }
}

std::vector<bool>::const_reference Individual::operator[](size_t index) const
{
    if (index < m_BinaryData.size())
    {
        return m_BinaryData[index];
    }
    else
    {
        throw libException::OutOfRangeException(index, m_BinaryData.size());
    }
}

std::vector<bool>::iterator Individual::begin()
{
    return m_BinaryData.begin();
}

std::vector<bool>::iterator Individual::end()
{
    return m_BinaryData.end();
}

std::vector<bool>::const_iterator Individual::begin() const
{
    return m_BinaryData.begin();
}

std::vector<bool>::const_iterator Individual::end() const
{
    return m_BinaryData.end();
}

size_t Individual::size() const
{
    return m_BinaryData.size();
}

bool Individual::operator==(const Individual &other) const
{
    return m_BinaryData == other.m_BinaryData;
}

bool Individual::operator!=(const Individual &other) const
{
    return !(this->operator==(other));
}

Individual& Individual::operator=(const Individual &other)
{
    if (size() == other.size())
    {
        m_BinaryData.assign(other.m_BinaryData.begin(), other.m_BinaryData.end());
        return *this;
    }
    else
    {
        throw InconsistentChromosomeLengthException(size(), other.size());
    }
}
}
