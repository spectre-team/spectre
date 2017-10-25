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
    GeneticTrainingSetSelectionScenario(double trainingSetSplitRate,
                                        double mutationRate,
                                        double bitSwapRate,
                                        double preservationRate,
                                        int generationsNumber,
                                        const std::vector<int>& populationSizes,
                                        const std::vector<int>& initialFillups,
                                        const std::string& reportFilename,
                                        int numberOfRestarts,
                                        Seed seed = 0);
    void execute(libClassifier::OpenCvDataset data);
private:
    const int mGenerationAmount;
    const std::vector<int> mGenerationSize;
    const std::vector<int> mTrueAmount;
    const double mTrainingRate;
    const double mMutationRate;
    const double mBitSwapRate;
    const double mPreservationRate;
    const std::string mFilename;
    const int mRunAmount;
    const Seed mSeed;
};

}
