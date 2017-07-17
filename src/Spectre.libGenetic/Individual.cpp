//#include "Individual.h"
//#include <vector>
//#include <ctime>
//#include <cstdlib>
//
//using namespace std;
//
//namespace Spectre::libGenetic
//{
//Individual::Individual(long size)
//{
//	this->size = size;
//	spectres.reserve(size);
//	srand(static_cast<unsigned int>(time(0)));
//	for (int i = 0; i<size; i++)
//	{
//		auto randomval = static_cast<bool>(rand() & 0x1);
//		spectres.push_back(randomval);
//	}
//}
//
//Individual::Individual(long size, long amount)
//{
//	this->size = size;
//	spectres.reserve(size);
//	srand(static_cast<unsigned int>(time(0)));
//	for (int i = 0; i<size; i++)
//	{
//		spectres.push_back(false);
//	}
//	for (int i = 0; i<amount; i++)
//	{
//        auto randomval = static_cast<bool>(rand() & 0x1);
//		spectres[randomval] = true;
//	}
//}
//
//Individual::Individual(long size, long amountFrom, long amountTo)
//{
//	this->size = size;
//	spectres.reserve(size);
//	srand(static_cast<unsigned int>(time(0)));
//	auto randomTrueVal = (rand() % (amountTo - amountFrom + 1)) + amountFrom;
//	for (int i = 0; i<size; i++)
//	{
//		spectres.push_back(false);
//	}
//	for (int i = 0; i<randomTrueVal; i++)
//	{
//		auto randomval = static_cast<bool>(rand() & 0x1);
//		spectres[randomval] = true;
//	}
//}
//}