#include "Generation.h"
#include "Individual.h"
#include "vector"

using namespace std;

Generation::Generation(long size, long indSize)
{
	this->size = size;
	specimen = new vector<Individual>(size, 0);
	int i;
	for (i = 0; i<size; i++)
	{
		specimen->push_back(new Individual(indSize));
	}
}

Generation::Generation(long size, long indSize, long amount)
{
	this->size = size;
	specimen = new vector<Individual>(size, 0);
	int i;
	for (i = 0; i<size; i++)
	{
		specimen->push_back(new Individual(indSize, amount));
	}
}

Generation::Generation(long size, long indSize, long amountFrom, long amountTo)
{
	this->size = size;
	specimen = new vector<Individual>(size, 0);
	int i;
	for (i = 0; i<size; i++)
	{
		specimen->push_back(new Individual(indSize, amountFrom, amountTo));
	}
}

Generation::~Generation()
{
	delete specimen;
}