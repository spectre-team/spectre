/*
* BilateralFilteringCli.h
* Bridge class for C# and native C++ for billateral filtering algorithm.
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
#include "AlgorithmCli.h"
#include "Spectre.HeatmapDataScallingAlgorithms/Algorithm.h"
#include "Spectre.HeatmapDataScallingAlgorithms/BilateralFiltering.h"

public ref class BilateralFilteringCli : AlgorithmCli
{
public:
	BilateralFilteringCli(array<double>^ managedIntensitiets, int numberOfRows, int numberOfColumns)
	{
		algorithm = new BilateralFiltering(numberOfRows, numberOfColumns);
		intensities = &toNative(managedIntensitiets);
	}

	BilateralFilteringCli(array<double>^ managedIntensitiets, int numberOfRows, int numberOfColumns, int window)
	{
		algorithm = new BilateralFiltering(numberOfRows, numberOfColumns, window);
		intensities = &toNative(managedIntensitiets);
	}

	virtual std::vector<double> scaleData() override
	{
		algorithm->scaleData(*intensities);
	}
};


