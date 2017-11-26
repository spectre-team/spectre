/*
* SupressionAlgorithm.cpp
* Class with suppresion algorithm implementation for contrast enhancement.
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

#include "SuppressionAlgorithm.h"
#include <algorithm>

SuppressionAlgorithm::SuppressionAlgorithm()
{
	this->topPercent = 0.01;
}

SuppressionAlgorithm::SuppressionAlgorithm(double topPercent)
{
	this->topPercent = topPercent;
}

SuppressionAlgorithm::~SuppressionAlgorithm() {}

std::vector<double> SuppressionAlgorithm::scaleData(std::vector<double> intensities)
{
	double maxIntensity = *max_element(std::begin(intensities), std::end(intensities));
	double cutoff = quantile(intensities, 1 - topPercent);
	for (auto &singleIntensity : intensities)
	{
		if (singleIntensity > cutoff)
			singleIntensity = cutoff;
		singleIntensity = maxIntensity * singleIntensity / cutoff;
	}
	return intensities;
}

double SuppressionAlgorithm::quantile(std::vector<double> intensities, double prob)
{
	size_t size = intensities.size();
	double index = 1 + (size - 1) * prob;
	int lo = floor(index);
	int hi = ceil(index);
	std::sort(intensities.begin(), intensities.end());
	double qs = intensities[lo - 1];
	if ((index > lo) && (intensities[hi - 1] != qs))
	{
		double h = index - lo;
		qs = (1 - h) * qs + h * intensities[hi - 1];
	}
	return qs;
}