#include "Individual.h"
#include <vector>
#include <ctime>
#include <cstdlib>

using namespace std;

namespace Spectre::libGenetic
{
Individual::Individual(long size)
{
	this->size = size;
	spectres = new vector<bool>(size, 0);
	int i, randomval;
	srand(time(0));
	for (i = 0; i<size; i++)
	{
		randomval = rand() % 2;
		spectres->push_back(randomval);
	}
}

Individual::Individual(long size, long amount)
{
	this->size = size;
	spectres = new vector<bool>(size, 0);
	int i, randomval;
	srand(time(0));
	for (i = 0; i<size; i++)
	{
		spectres->push_back(false);
	}
	for (i = 0; i<amount; i++)
	{
		randomval = rand() % size;
		spectres->at(randomval) = true;
	}
}

Individual::Individual(long size, long amountFrom, long amountTo)
{
	this->size = size;
	spectres = new vector<bool>(size, 0);
	int i, randomval, randomTrueVal;
	srand(time(0));
	randomTrueVal = (rand() % (amountTo - amountFrom + 1)) + amountFrom;
	for (i = 0; i<size; i++)
	{
		spectres->push_back(false);
	}
	for (i = 0; i<randomTrueVal; i++)
	{
		randomval = rand() % size;
		spectres->at(randomval) = true;
	}
}

Individual::~Individual()
{
	delete spectres;
}
}