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
