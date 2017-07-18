#include <vector>
#include "Individual.h"

using namespace std;

namespace Spectre::libGenetic
{
Individual::Individual(std::vector<bool>&& binaryData):
    m_BinaryData(binaryData)
{
	
}

std::vector<bool>::reference Individual::operator[](size_t index)
{
    return m_BinaryData[index];
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

}