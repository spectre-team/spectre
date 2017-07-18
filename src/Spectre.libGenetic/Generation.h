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
	virtual ~Generation() = default;

private:
	std::vector<Individual> m_Generation;
};
}
