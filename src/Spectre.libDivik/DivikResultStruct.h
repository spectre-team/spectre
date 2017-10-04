/*
* DivikResultStruct.h
* Contains native structure aggregating all the output from DiviK algorithm.
*
Copyright 2017 Dariusz Kuchta

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

typedef std::vector<double> Centroid;

struct DivikResultStruct
{
    DivikResultStruct();

    double qualityIndex;
    std::vector<Centroid> centroids;
    std::vector<int> partition;

    double amplitudeThreshold;
    std::vector<bool> amplitudeFilter;

    double varianceThreshold;
    std::vector<bool> varianceFilter;

    std::vector<int> merged;

    std::vector<DivikResultStruct> subregions;
};
