/*
* HeatmapDataScalingAlgorithm.h
* Abstract class with scaleData virtual method.
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
#include <vector>
#include "Spectre.libHeatmapDataScaling/HeatmapDataScalingAlgorithm.h"

namespace Spectre::HeatmapDataScalingCli 
{
	public ref class HeatmapDataScalingAlgorithm
	{
	public:
        HeatmapDataScalingAlgorithm(Spectre::libHeatmapDataScaling::HeatmapDataScalingAlgorithm* _heatmapDataScalingAlgorithm)
	        : heatmapDataScalingAlgorithm(_heatmapDataScalingAlgorithm){ }

        ~HeatmapDataScalingAlgorithm()
        {
            delete heatmapDataScalingAlgorithm;
        }

		array<double>^ scaleData(array<double>^ managedIntensitiets)
		{
            std::vector<double> nativeSource(managedIntensitiets->Length);
            for (auto i = 0u; i< nativeSource.size(); ++i)
            {
                nativeSource[i] = managedIntensitiets[i];
            }

            auto nativeResult = heatmapDataScalingAlgorithm->scaleData(nativeSource);

            //translate native to managed
            array<double>^ managedResult = gcnew array<double>(static_cast<int>(nativeResult.size()));
            for (auto i = 0u; i<nativeResult.size(); ++i)
            {
                managedResult[i] = nativeResult[i];
            }

            return managedResult;
		}
	private:
        Spectre::libHeatmapDataScaling::HeatmapDataScalingAlgorithm* heatmapDataScalingAlgorithm;
	};
}
