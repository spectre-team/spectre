/*
* Normalization.h
* Bridge class for C# and native C++ for normalization algorithm.
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
#include "Spectre.libHeatmapDataScaling/Normalization.h"
#include "Spectre.libHeatmapDataScaling/HeatmapDataScalingAlgorithm.h"
#include "IntensitiesDataConverter.h"
#include <iostream>

#include "NativeProcessor.h"

std::vector<double> testMap(gsl::span<double> src)
{
    return std::vector<double>(src.begin(), src.end());
}

namespace Spectre::HeatmapDataScalingCli
{
	public ref class Normalization : HeatmapDataScalingAlgorithm
	{
	public:
		Normalization() : HeatmapDataScalingAlgorithm(new Spectre::libHeatmapDataScaling::Normalization()) { }

        Normalization(const int min, const int max) : HeatmapDataScalingAlgorithm(new Spectre::libHeatmapDataScaling::Normalization(min, max)) { }
	};
}
