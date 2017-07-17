#include <vector>
#include "Generation.h"
//#include "Individual.h"

using namespace std;

namespace Spectre::libGenetic
{
Generation::Generation(long size, long indSize)
{
    throw std::exception("Not implemented");
	/*this->size = size;
	specimen.reserve(size);
	for (int i = 0; i < size; i++)
	{
		specimen.push_back(Individual(indSize));
	}*/
}

Generation::Generation(long size, long indSize, long amount)
{
    throw std::exception("Not implemented");
	/*this->size = size;
	specimen.reserve(size);
	for (int i = 0; i  <size; i++)
	{
		specimen.push_back(Individual(indSize, amount));
	}*/
}

Generation::Generation(long size, long indSize, long amountFrom, long amountTo)
{
    throw std::exception("Not implemented");
	/*this->size = size;
	specimen.reserve(size);
	for (int i = 0; i < size; i++)
	{
		specimen.push_back(Individual(indSize, amountFrom, amountTo));
	}*/
}
}
