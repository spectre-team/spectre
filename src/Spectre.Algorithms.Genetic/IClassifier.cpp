#include "IClassifier.h"
#include "Individual.h"

public class IClassifier
{
public:
	IClassifier();
	~IClassifier();
	virtual long getScore(Individual individual);

private:

};

IClassifier::IClassifier()
{
}

IClassifier::~IClassifier()
{
}