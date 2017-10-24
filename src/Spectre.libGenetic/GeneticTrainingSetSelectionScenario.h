/*
* GeneticAlgorithmExecutor.h
* Executes whole genetic algorithm and returns results.
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
#include "Spectre.libClassifier/OpenCvDataset.h"
#include "Spectre.libGenetic/DataTypes.h"

namespace Spectre::libGenetic {

class GeneticTrainingSetSelectionScenario
{
public:
    GeneticTrainingSetSelectionScenario(double trainingRate, double mutationRate, double bitSwapRate, double preservationRate, int generationAmount, int generationSize, int trueAmount, std::string filename, Seed seed = 0);
    Generation execute(libClassifier::OpenCvDataset data);
private:
    int mGenerationAmount;
    int mGenerationSize;
    int mTrueAmount;
    double mTrainingRate;
    double mMutationRate;
    double mBitSwapRate;
    double mPreservationRate;
    std::string mFilename;
    Seed mSeed;
};

}
