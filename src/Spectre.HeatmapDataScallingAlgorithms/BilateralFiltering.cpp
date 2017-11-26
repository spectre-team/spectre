/*
* BilateralFiltering.cpp
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

#include "BilateralFiltering.h"

BilateralFiltering::BilateralFiltering(int numberOfRows, int numberOfColumns)
{
	this->window = 3;
	this->numberOfRows = numberOfRows;
	this->numberOfColumns = numberOfColumns;
}

BilateralFiltering::BilateralFiltering(int numberOfRows, int numberOfColumns, int window)
{
	this->window = window;
	this->numberOfRows = numberOfRows;
	this->numberOfColumns = numberOfColumns;
}

BilateralFiltering::~BilateralFiltering()
{
}

std::vector<double> BilateralFiltering::scaleData(std::vector<double> intensities)
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

	return bilateralFilter(intensities, sd, r, beta);
}

std::vector<double> BilateralFiltering::bilateralFilter(std::vector<double> intensities, double sd, int r, std::vector<double> beta)
{
	beta = bilateralWeights(intensities, beta, r);
	return gaussianFilter(intensities, sd, r, beta);
}

std::vector<double> BilateralFiltering::bilateralWeights(std::vector<double> intensities, std::vector<double> beta, int r)
{
	int window = pow((2 * (r)) + 1, 2);
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
					beta[ix * window + k] = fabs(intensities[(j * (numberOfRows)) + i]
						- intensities[(temp_j * (numberOfRows)) + temp_i]);
					++k;
				}
			}
			double max_beta = 0;
			double min_beta = beta[ix * window];
			for (int l = 0; l < window; ++l) {
				if (beta[ix * window + l] > max_beta)
					max_beta = beta[ix * window + l];
				if (beta[ix * window + l] < min_beta)
					min_beta = beta[ix * window + l];
			}
			double lambda = (max_beta - min_beta) / 2.0;
			if (lambda < 1e-9) {
				lambda = 1.0;
			}
			lambda = lambda * lambda;
			for (int l = 0; l < window; ++l) {
				beta[ix * window + l] = exp(-pow(beta[ix * window + l], 2) / (2.0 * lambda));
			}
			++ix;
		}
	}
	return beta;
}

std::vector<double> BilateralFiltering::gaussianFilter(std::vector<double> intensities, double sd, int r, std::vector<double> beta)
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



