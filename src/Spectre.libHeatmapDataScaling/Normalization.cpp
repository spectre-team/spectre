/*
* Normalization.cpp
* Class with normalization algorithm implementation.
*
Copyright 2017 Daniel Babiak
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

#include "Normalization.h"
#include <algorithm>
#include <vector>

namespace Spectre::libHeatmapDataScaling
{
	Normalization::Normalization(const int _min, const int _max) 
		: min(_min), max(_max) { };



	std::vector<double> *Normalization::scaleData(const gsl::span<double> intensities)
	{
		std::vector<double> *newIntensities = new std::vector<double>();
		newIntensities->reserve(intensities.size());
		double oldMin = *min_element(std::begin(intensities), std::end(intensities));
		double oldMax = *max_element(std::begin(intensities), std::end(intensities));

		for (int i = 0; i< newIntensities->size(); i++)
		{
			(*newIntensities)[i] = ((intensities[i] - oldMin) * (max - min) / (oldMax - oldMin)) + min;
		}
		return newIntensities;
	}
}