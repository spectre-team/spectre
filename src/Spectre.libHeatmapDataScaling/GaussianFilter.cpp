/*
* GaussianFilter.h
* Class which implements algorithm for filtering data with Gaussian function.
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

#include "GaussianFilter.h"

namespace Spectre::libHeatmapDataScaling
{
std::vector<double> *GaussianFilter::filterDataWithGaussianFunction(const gsl::span<double> &intensities, const int numberOfRows, const int numberOfColumns,
	const double sd, const int r, const gsl::span<double> &beta)
{
	std::vector<double> *newIntensities = new std::vector<double>(intensities.size());
	int gaussianKernel = (int)pow((2 * (r)) + 1, 2);
	double gamma_raw[9];
	int ix = 0;
	for (int i = 0; i < numberOfRows; ++i) {
		for (int j = 0; j < numberOfColumns; ++j) {
			int k = 0;
			double gamma_sum = 0;
			for (int ii = -(r); ii <= r; ++ii) {
				for (int jj = -(r); jj <= r; ++jj) {
					double alpha = exp(-(double)(ii*ii + jj*jj) / (2.0 * pow(sd, 2)));
					gamma_raw[k] = alpha * beta[ix * gaussianKernel + k];
					gamma_sum += gamma_raw[k];
					++k;
				}
			}
			k = 0;
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
					double gamma = gamma_raw[k] / gamma_sum;
					(*newIntensities)[(j * (numberOfRows)) + i] += gamma * intensities[(temp_j * (numberOfRows)) + temp_i];
					++k;
				}
			}
			++ix;
		}
	}
	return newIntensities;
}
}