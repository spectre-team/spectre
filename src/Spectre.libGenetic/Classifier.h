#pragma once

namespace Spectre::libGenetic
{
class Individual;

class Classifier
{
public:
	Classifier();
    virtual ~Classifier();
	virtual long getScore(Individual individual);

private:

};
}
