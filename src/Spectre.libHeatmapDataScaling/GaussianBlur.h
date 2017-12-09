/*
* GaussianBlur.h
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

#pragma once
#include "HeatmapDataScalingAlgorithm.h"
#include "GaussianFilter.h"

namespace Spectre::libHeatmapDataScaling
{
	class GaussianBlur : public HeatmapDataScalingAlgorithm
	{
	public:
		GaussianBlur(const int _numberOfRows, const int _numberOfColumns, const int _window = 3);
		std::vector<double> *scaleData(const gsl::span<double> intensities) override;
	private:
		const int window;
		const int numberOfRows;
		const int numberOfColumns;
	};
}
