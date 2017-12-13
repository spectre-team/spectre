/*
* GaussianBlur.cpp
* Class with gaussian blur algorithm implementation.
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

#include "GaussianBlur.h"

namespace Spectre::libHeatmapDataScaling
{
	GaussianBlur::GaussianBlur(const int _numberOfRows, const int _numberOfColumns, const int _window)
		: numberOfRows(_numberOfRows), numberOfColumns(_numberOfColumns), window(_window) { }

	std::vector<double> GaussianBlur::scaleData(const gsl::span<double> intensities)
	{
		int r = (int)floor(window / 2);
		double sd = window / 4.0;
		int nrow = (int)pow((2 * (r)) + 1, 2);
		size_t ncol = intensities.size();
        std::vector<double> beta;
		size_t betaSize = nrow*ncol;
        beta.reserve(betaSize);
		for (int i = 0; i < betaSize; i++)
		{
			beta.push_back(1);
		}

		return *gaussianFilter.filterDataWithGaussianFunction(intensities, numberOfRows, numberOfColumns, sd, r, beta);
	}
}