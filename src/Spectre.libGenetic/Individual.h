#pragma once
#include "vector"

class Individual
{
public:
	Individual(long size);
	Individual(long size, long amount);
	Individual(long size, long amountFrom, long amountTo);
	~Individual();

private:
	long size;
	std::vector<bool>* spectres;
};
