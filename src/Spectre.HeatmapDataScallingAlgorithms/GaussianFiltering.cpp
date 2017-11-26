/*
* GaussianFiltering.cpp
* Class with gaussian filtering algorithm implementation.
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

#include "GaussianFiltering.h"

GaussianFiltering::GaussianFiltering(int numberOfRows, int numberOfColumns)
{
	this->window = 3;
	this->numberOfRows = numberOfRows;
	this->numberOfColumns = numberOfColumns;
}

GaussianFiltering::GaussianFiltering(int numberOfRows, int numberOfColumns, int window)
{
	this->window = window;
	this->numberOfRows = numberOfRows;
	this->numberOfColumns = numberOfColumns;
}

GaussianFiltering::~GaussianFiltering()
{
}

std::vector<double> GaussianFiltering::scaleData(std::vector<double> intensities)
{
	int r = floor(window / 2);
	double sd = window / 4;
	int nrow = pow((2 * (r)) + 1, 2);
	int ncol = intensities.size();
	std::vector<double> beta;
	int betaSize = nrow*ncol;
	for (int i = 0; i < betaSize; i++)
	{
		beta.push_back(1);
	}
	
	return gaussianFilter(intensities, sd, r, beta);
}

std::vector<double> GaussianFiltering::gaussianFilter(std::vector<double> intensities, double sd, int r, std::vector<double> beta)
{
	std::vector<double> newIntensities(intensities.size());
	int window = pow((2 * (r)) + 1, 2);
	double gamma_raw[9];
	int ix = 0;
	for (int i = 0; i < numberOfRows; ++i) {
		for (int j = 0; j < numberOfColumns; ++j) {
			int k = 0;
			double gamma_sum = 0;
			for (int ii = -(r); ii <= r; ++ii) {
				for (int jj = -(r); jj <= r; ++jj) {
					double alpha = exp(-(double)(ii*ii + jj*jj) / (2.0 * pow(sd, 2)));
					gamma_raw[k] = alpha * beta[ix * window + k];
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
					newIntensities[(j * (numberOfRows)) + i] += gamma * intensities[(temp_j * (numberOfRows)) + temp_i];
					++k;
				}
			}
			++ix;
		}
	}
	return newIntensities;
}