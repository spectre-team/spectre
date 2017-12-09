/*
* BilateralBlur.h
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
#include "HeatmapDataScalingAlgorithm.h"
#include "Spectre.libHeatmapDataScaling/HeatmapDataScalingAlgorithm.h"
#include "Spectre.libHeatmapDataScaling/BilateralBlur.h"
#include "IntensitiesDataConverter.h"

namespace Spectre::HeatmapDataScalingCli
{
	public ref class BilateralBlur : HeatmapDataScalingAlgorithm
	{
	public:
		BilateralBlur(array<double>^ managedIntensitiets, const int numberOfRows, const int numberOfColumns)
		{
			heatmapDataScalingAlgorithm = new Spectre::libHeatmapDataScaling::BilateralBlur(numberOfRows, numberOfColumns);
			intensities = IntensitiesDataConverter::toNative(managedIntensitiets);
		}

		BilateralBlur(array<double>^ managedIntensitiets, const int numberOfRows, const int numberOfColumns, const int window)
		{
			heatmapDataScalingAlgorithm = new Spectre::libHeatmapDataScaling::BilateralBlur(numberOfRows, numberOfColumns, window);
			intensities = IntensitiesDataConverter::toNative(managedIntensitiets);
		}

		//TODO: Memory lakes (heatmapDataScalingAlgorithm, intensities), return correct double array from native C++
		virtual array<double>^scaleData()
		{
			intensities = heatmapDataScalingAlgorithm->scaleData(*intensities);
			delete heatmapDataScalingAlgorithm;
			return gcnew array<double>(12);
		}
	private:
		Spectre::libHeatmapDataScaling::HeatmapDataScalingAlgorithm * heatmapDataScalingAlgorithm;
		std::vector<double> * intensities;
	};
}

