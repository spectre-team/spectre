#pragma once
#include "DataTypes.h"

namespace Spectre::libGenetic
{
class Classifier
{
public:
	Classifier();
    virtual ~Classifier();
	virtual long getScore(Individual individual);

private:

};
}
