#pragma once
#include "Generation.h"

class IDataset;

class ISelection
{
public:
	ISelection();
	~ISelection();
	virtual Generation performSelection(Generation generation, long size);
	virtual Generation performSelection(Generation generation, long size, long includedFrom = 100, long includedTo = 100);
	virtual Generation performSelection(IDataset data, long size, long includedFrom = 100, long includedTo = 100);
private:

};
