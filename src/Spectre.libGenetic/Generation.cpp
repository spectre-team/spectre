#include <vector>
#include <algorithm>
#include "Generation.h"

using namespace std;

namespace Spectre::libGenetic
{
Generation::Generation(std::vector<Individual>&& generation):
    m_Generation(generation)
{
    const auto minmax = std::minmax_element(m_Generation.begin(), m_Generation.end(), [](const auto& first, const auto& second) { return first.size() < second.size(); });
    const auto minLength = minmax.first->size();
    const auto maxLength = minmax.second->size();
    if(minLength == maxLength)
    {
        
    }
    else
    {
        throw std::exception("Inconsistent input size");
    }
}

Generation Generation::operator+(const Generation& other) const
{
    std::vector<Individual> newAllocation;
    newAllocation.reserve(this->m_Generation.size() + other.m_Generation.size());
    newAllocation.insert(newAllocation.end(), this->m_Generation.begin(), this->m_Generation.end());
    newAllocation.insert(newAllocation.end(), other.m_Generation.begin(), other.m_Generation.end());
    Generation generation(std::move(newAllocation));
    return generation;
}

Generation& Generation::operator+=(const Generation& other)
{
    this->m_Generation = (*this + other).m_Generation;
    return *this;
}

size_t Generation::size() const
{
    return m_Generation.size();
}

const Individual& Generation::operator[](size_t index) const
{
    return m_Generation[index];
}

}
