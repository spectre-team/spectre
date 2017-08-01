/*
* DivikResult.cpp
* Contains implementation of methods of structure organizing all 
* the output from DiviK algorithm.
*
Copyright 2017 Dariusz Kuchta

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

#include "stdafx.h"
#include "DivikResult.h"

#include <cliext\algorithm>

using System::Object;

//TODO: move to utilities
template <typename T, int R>
static bool UnmanagedSequenceEqual(array<T, R>^ lhs, array<T, R>^ rhs)
{
	if (System::Object::ReferenceEquals(lhs, rhs))
		return true;
	
	if ((lhs->Length != rhs->Length) || (lhs->Rank != rhs->Rank))
		return false;

	for (int i = 0; i < lhs->Rank; i++)
		for (int j = 0; j < lhs->Length; j++)
			if (lhs[i, j] != rhs[i, j])
				return false;

	return true;
}

template <typename T>
static bool UnmanagedSequenceEqual(array<T, 1>^ lhs, array<T, 1>^ rhs)
{
	if (System::Object::ReferenceEquals(lhs, rhs))
		return true;

	if ((lhs->Length != rhs->Length) || (lhs->Rank != rhs->Rank))
		return false;

	for (int i = 0; i < lhs->Length; i++)
		if (lhs[i] != rhs[i])
			return false;

	return true;
}

namespace Spectre::AlgorithmsCli::Results
{
	bool DivikResult::Equals(Object %obj)
	{
		if (!(obj.GetType() == this->GetType()))
			return false;
		if (ReferenceEquals(this, %obj))
			return true;

		auto other = (DivikResult^) % obj;

		if ((isnan<double>(other->AmplitudeThreshold) != isnan<double>(this->AmplitudeThreshold)) || (!isnan<double>(this->AmplitudeThreshold) && other->AmplitudeThreshold != this->AmplitudeThreshold))
			return false;
		if ((isnan<double>(other->VarianceThreshold) != isnan<double>(this->VarianceThreshold)) || (!isnan<double>(this->VarianceThreshold) && other->VarianceThreshold != this->VarianceThreshold))
			return false;
		if (other->QualityIndex != this->QualityIndex)
			return false;
		// TODO: Find potential C++/CLI equivalents of SequenceEqual
		if (!UnmanagedSequenceEqual(other->AmplitudeFilter, this->AmplitudeFilter))
			return false;
		if (!UnmanagedSequenceEqual(other->VarianceFilter, this->VarianceFilter))
			return false;
		if (!UnmanagedSequenceEqual(other->Partition, this->Partition))
			return false;
		if (!UnmanagedSequenceEqual(other->Merged, this->Merged))
			return false;
		if (!UnmanagedSequenceEqual(other->Centroids, this->Centroids))
			return false;
		if (!UnmanagedSequenceEqual(other->Subregions, this->Subregions))
			return false;

		return true;
	}

	int DivikResult::GetHashCode()
	{
		int hash = 17;
		hash = 23 * hash + AmplitudeThreshold.GetHashCode();
		hash = 23 * hash + VarianceThreshold.GetHashCode();
		hash = 23 * hash + QualityIndex.GetHashCode();
		hash = 23 * hash + AmplitudeFilter->GetHashCode();
		hash = 23 * hash + VarianceFilter->GetHashCode();
		hash = 23 * hash + Centroids->GetHashCode();
		hash = 23 * hash + Partition->GetHashCode();
		hash = 23 * hash + Merged->GetHashCode();
		hash = 23 * hash + Subregions->GetHashCode();

		return hash;
	}

}
