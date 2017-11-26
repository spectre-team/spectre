/*
* BilateralFiltering.h
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

#pragma once
#include "Algorithm.h"

class BilateralFiltering : public Algorithm
{
private:
	int window;
	int numberOfRows;
	int numberOfColumns;
	std::vector<double> bilateralFilter(std::vector<double> intensities, double sd, int r, std::vector<double> beta);
	std::vector<double> BilateralFiltering::bilateralWeights(std::vector<double> intensities, std::vector<double> beta, int r);
	std::vector<double> BilateralFiltering::gaussianFilter(std::vector<double> intensities, double sd, int r, std::vector<double> beta);
public:
	BilateralFiltering(int numberOfRows, int numberOfColumns);
	BilateralFiltering(int numberOfRows, int numberOfColumns, int window);
	~BilateralFiltering();
	virtual std::vector<double> scaleData(std::vector<double> intensities);
};