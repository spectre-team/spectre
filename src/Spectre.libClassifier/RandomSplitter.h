/*
* RandomSplitter.h
* 
*
Copyright 2017 Spectre Team

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
#include <utility>
#include "Spectre.libClassifier/OpenCvDataset.h"
#include <random>

namespace Spectre::libClassifier {

class RandomSplitter
{

using RandomNumberGenerator = std::mt19937_64;
using Seed = _ULonglong;

public:
    RandomSplitter::RandomSplitter(double trainingPercent, Seed rngSeed = 0);
    std::pair<Spectre::libClassifier::OpenCvDataset, Spectre::libClassifier::OpenCvDataset> RandomSplitter::split(const Spectre::libClassifier::OpenCvDataset& data);
private:
    double m_trainingPercent;
    RandomNumberGenerator m_randomNumberGenerator;
};

}
