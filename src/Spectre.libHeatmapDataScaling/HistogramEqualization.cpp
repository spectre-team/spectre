/*
* HistogramEqualization.cpp
* Class with histogram equalization algorithm implementation for contrast enhancement.
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

#include "HistogramEqualization.h"

namespace Spectre::libHeatmapDataScaling
{
    std::vector<double> HistogramEqualization::scaleData(const gsl::span<double> intensities)
    {
        std::vector<double> roundedIntensities = roundIntensities(intensities);
        std::map<double, unsigned int> histogramMap = calculateCumulativeDistribution(countRepeatingValues(roundedIntensities));
        return calculateNewHistogramData(roundedIntensities, histogramMap);
    }

    std::vector<double> HistogramEqualization::roundIntensities(const gsl::span<double> intensities)
    {
        std::vector<double> roundedIntensities;
        for (auto intensity : intensities)
        {
            roundedIntensities.push_back(round(intensity));
        }
        return roundedIntensities;
    }

    std::map<double, unsigned int> HistogramEqualization::countRepeatingValues(const gsl::span<double> intensities) {
        std::map<double, unsigned int> histogramMap;
        for (auto intensity : intensities)
        {
            histogramMap[intensity]++;
        }
        return histogramMap;
    }

    std::map<double, unsigned int> HistogramEqualization::calculateCumulativeDistribution(std::map<double, unsigned int> histogramMap)
    {
        std::map<double, unsigned int> cumulativeDistributionMap;
        unsigned int temporaryDistributionValue = 0;

        for (auto element : histogramMap)
        {
            temporaryDistributionValue += element.second;
            cumulativeDistributionMap.emplace(element.first, temporaryDistributionValue);
        }
        return  cumulativeDistributionMap;

    }

    std::vector<double> HistogramEqualization::calculateNewHistogramData(const gsl::span<double> intensities, std::map<double, 
        unsigned int> histogramMap)
    {
        std::vector<double> newIntensities;
        std::pair<double, unsigned int> const min = (*std::min_element(histogramMap.begin(), histogramMap.end(),
            [](decltype(histogramMap)::value_type& l, decltype(histogramMap)::value_type& r) -> bool { return l.second < r.second; }));
        size_t const size = intensities.size();
        for (auto i = 0; i < size; i++)
        {
            newIntensities.push_back((histogramMap.find(intensities[i])->second - static_cast<double>(min.second)) / 
                (size- static_cast<double>(min.second)) * 255);
        }

        return newIntensities;
    }
}
