#include "Generation.h"
#include "Individual.h"
#include <vector>

using namespace std;

namespace Spectre::libGenetic
{
Generation::Generation(long size, long indSize)
{
	this->size = size;
	specimen.reserve(size);
	for (int i = 0; i < size; i++)
	{
		specimen.push_back(Individual(indSize));
	}
}

Generation::Generation(long size, long indSize, long amount)
{
	this->size = size;
	specimen.reserve(size);
	for (int i = 0; i  <size; i++)
	{
		specimen.push_back(Individual(indSize, amount));
	}
}

Generation::Generation(long size, long indSize, long amountFrom, long amountTo)
{
	this->size = size;
	specimen.reserve(size);
	for (int i = 0; i < size; i++)
	{
		specimen.push_back(Individual(indSize, amountFrom, amountTo));
	}
}
}
