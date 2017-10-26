/*
* GeneticTrainingSetSelectionScenario.h
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
#include "Spectre.libGenetic/GeneticAlgorithmFactory.h"

namespace Spectre::GaSvmNative
{
class GeneticTrainingSetSelectionScenario
{
public:
    GeneticTrainingSetSelectionScenario(double trainingSetSplitRate,
                                        double mutationRate,
                                        double bitSwapRate,
                                        double preservationRate,
                                        unsigned int generationsNumber,
                                        const std::vector<unsigned int>& populationSizes,
                                        const std::vector<unsigned int>& initialFillups,
                                        const std::string& reportFilename,
                                        unsigned int numberOfRestarts,
                                        unsigned int numberOfCores=1u,
                                        libGenetic::Seed seed = 0,
                                        size_t minimalFillup=1ul,
                                        size_t maximalFillup=std::numeric_limits<size_t>::max());
    void execute(libClassifier::OpenCvDataset data) const;
private:
    const std::vector<unsigned int> m_PopulationSizes;
    const std::vector<unsigned int> m_InitialIndividualFillups;
    const double m_TrainingDatasetSizeRate;
    const std::string m_Filename;
    const unsigned int m_RestartsNumber;
    const unsigned int m_NumberOfCores;
    const libGenetic::Seed m_Seed;
    const libGenetic::GeneticAlgorithmFactory m_GaFactory;
};
}
