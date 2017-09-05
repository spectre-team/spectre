/*
* RandomSplitter.h
* Splits OpenCVDataset to training set and test set
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
#include <random>
#include "Spectre.libClassifier/OpenCvDataset.h"
#include "Spectre.libClassifier/SplittedOpevCvDataset.h"
#include "Spectre.libGenetic/DataTypes.h"

namespace Spectre::libClassifier {

class RandomSplitter
{

public:
    RandomSplitter::RandomSplitter(double trainingPercent, libGenetic::Seed rngSeed = 0);
    SplittedOpenCvDataset RandomSplitter::split(const Spectre::libClassifier::OpenCvDataset& data);
private:
    double m_trainingProbability;
    libGenetic::RandomNumberGenerator m_randomNumberGenerator;
};

}
