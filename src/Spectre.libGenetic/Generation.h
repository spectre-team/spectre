#pragma once
#include "vector"
#include "Individual.h"

namespace Spectre::libGenetic
{
class Generation
{
public:
	Generation(long size, long indSize);
	Generation(long size, long indSize, long amount);
	Generation(long size, long indSize, long amountFrom, long amountTo);
    Generation operator+(const Generation& other);
    Generation operator=(Generation&& other);
    Generation operator+=(const Generation& other);
	virtual ~Generation() = default;

private:
	long size;
	std::vector<Individual> specimen;
};
}
