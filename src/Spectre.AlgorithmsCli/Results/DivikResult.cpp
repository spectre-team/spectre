#include "stdafx.h"
#include "DivikResult.h"

#include <cliext\algorithm>

bool Divik::DivikResultTest::Equals(Object %obj)
{
	if(!(obj.GetType() == this->GetType()))
		return false;
	if (ReferenceEquals(this, %obj))
		return true;

	auto other = (DivikResultTest^)%obj;

	if ((isnan<double>(other->AmplitudeThreshold) != isnan<double>(this->AmplitudeThreshold)) || (!isnan<double>(this->AmplitudeThreshold) && other->AmplitudeThreshold != this->AmplitudeThreshold))
		return false;
	if ((isnan<double>(other->VarianceThreshold) != isnan<double>(this->VarianceThreshold)) || (!isnan<double>(this->VarianceThreshold) && other->VarianceThreshold != this->VarianceThreshold))
		return false;
	if (other->QualityIndex != this->QualityIndex)
		return false;
	if (other->AmplitudeFilter->Length != this->AmplitudeFilter->Length)
		return false;
	if (other->VarianceFilter->Length != this->VarianceFilter->Length)
		return false;
	if (other->Centroids->Length != this->Centroids->Length)
		return false;
	if (other->Partition->Length != this->Partition->Length)
		return false;
	// TODO: Find C++/CLI equivalents of SequenceEqual
	if (!other->AmplitudeFilter->SequenceEqual(this->AmplitudeFilter))
		return false;
	if (!other->VarianceFilter->SequenceEqual(this->VarianceFilter))
		return false;
	if (!other->Partition->SequenceEqual(this->Partition))
		return false;
	if ((other->Merged != null) != (this->Merged != null))
		return false;
	if (!other->Merged->SequenceEqual(this->Merged))
		return false;
	if (!other->Centroids.Cast<double>().SequenceEqual(this->Centroids.Cast<double>()))
		return false;
	if (!other->Subregions->SequenceEqual(this->Subregions))
		return false;
}
