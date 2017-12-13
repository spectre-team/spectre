/*
* BilateralBlur.cpp
* Class with bilateral filtering algorithm implementation.
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

#include "BilateralBlur.h"

namespace Spectre::libHeatmapDataScaling
{

	BilateralBlur::BilateralBlur(const int _numberOfRows, const int _numberOfColumns, const int _window)
		: numberOfRows(_numberOfRows), numberOfColumns(_numberOfColumns), window(_window) { }

	std::vector<double> BilateralBlur::scaleData(const gsl::span<double> intensities)
	{
		int r = (int)floor(window / 2);
		double sd = window / 4;
		int nrow = (int)pow((2 * (r)) + 1, 2);
		size_t ncol = intensities.size();
		std::vector<double> beta;
		size_t betaSize = nrow*ncol;
		beta.reserve(betaSize);
		for (int i = 0; i < betaSize; i++)
		{
			beta[i] = 1;
		}

		beta = calculateWeightsForBilateralBlur(intensities, beta, r);
		return *gaussianFilter.filterDataWithGaussianFunction(intensities, numberOfRows, numberOfColumns, sd, r, beta);
	}

	std::vector<double> &BilateralBlur::calculateWeightsForBilateralBlur(const gsl::span<double> intensities, std::vector<double> &beta, const int r)
	{
		int gaussianKernel = (int)pow((2 * (r)) + 1, 2);
		int ix = 0;
		for (int i = 0; i < numberOfRows; ++i) {
			for (int j = 0; j < numberOfColumns; ++j) {
				int k = 0;
				for (int ii = -(r); ii <= r; ++ii) {
					for (int jj = -(r); jj <= r; ++jj) {
						int temp_i = i + ii;
						int temp_j = j + jj;
						if (temp_i < 0 || temp_i > numberOfRows - 1) {
							temp_i = i;
						}
						if (temp_j < 0 || temp_j > numberOfColumns - 1) {
							temp_j = j;
						}
						if (intensities[(temp_j * (numberOfRows)) + temp_i] < 0) {
							temp_i = i;
							temp_j = j;
						}
						beta[ix * gaussianKernel + k] = fabs(intensities[(j * (numberOfRows)) + i]
							- intensities[(temp_j * (numberOfRows)) + temp_i]);
						++k;
					}
				}
				double max_beta = 0;
				double min_beta = beta[ix * gaussianKernel];
				for (int l = 0; l < gaussianKernel; ++l) {
					if (beta[ix * gaussianKernel + l] > max_beta)
						max_beta = beta[ix * gaussianKernel + l];
					if (beta[ix * gaussianKernel + l] < min_beta)
						min_beta = beta[ix * gaussianKernel + l];
				}
				double lambda = (max_beta - min_beta) / 2.0;
				if (lambda < 1e-9) {
					lambda = 1.0;
				}
				lambda = lambda * lambda;
				for (int l = 0; l < gaussianKernel; ++l) {
					beta[ix * gaussianKernel + l] = exp(-pow(beta[ix * gaussianKernel + l], 2) / (2.0 * lambda));
				}
				++ix;
			}
		}
		return beta;
	}
}


