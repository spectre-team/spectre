#pragma once

namespace Spectre::libGenetic
{
class Individual;

class IClassifier
{
public:
	IClassifier();
	~IClassifier();
	virtual long getScore(Individual individual);

private:

};
}
