/*
* HistogramEqualization.h
* Bridge class for C# and native C++ for histogram equalization algorithm.
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
#include "Spectre.libHeatmapDataScaling/HeatmapDataScalingAlgorithm.h"
#include "Spectre.libHeatmapDataScaling/HistogramEqualization.h"
#include "HeatmapDataScalingAlgorithm.h"

namespace Spectre::HeatmapDataScalingCli
{
    public ref class HistogramEqualization : HeatmapDataScalingAlgorithm
    {
    public:
        HistogramEqualization() : HeatmapDataScalingAlgorithm(new Spectre::libHeatmapDataScaling::HistogramEqualization()) { }
    };
}
